using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float currentHealth, maxHealth;
    public float speed;
    public float maxDamage;
    public float attackSpeed, attackDelay;
    Transform player;
    Transform focus;
    Rigidbody rb;
    public float suspiciousThreshold, hostileThreshold, attackThreshold;
    Collider col;

    #region Patrol
    NavMeshAgent agent;
    public Transform[] routes;
    public int currentRoute, nextRoute;
    public float routeThreshold;
    #endregion

    #region AI
    public enum AIState { idle, searching, hostile, frenzy, dead }
    public AIState state;
    bool frenzy = false;
    bool dead = false;
    bool dooted = false;
    #endregion

    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
        state = AIState.idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    protected void Update()
    {
        if(!dooted)
            StateCheck();
        if (currentHealth <= maxHealth * 0.2f && !frenzy)
            TriggerFrenzy();
        if (currentHealth <= 0 && !dead)
            TriggerDead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dooted && !dead)
            EnvironmentalDamage(collision);
    }

    void EnvironmentalDamage(Collision collision)
    {
        Debug.Log(name + " collided with " + collision.collider.name + " for " + rb.velocity.magnitude.ToString());
        currentHealth -= rb.velocity.magnitude;
    }

    void StateCheck()
    {
        if (state == AIState.idle)
            IdleUpdate();
        if (state == AIState.searching)
            SearchingUpdate();
        if(state == AIState.hostile)
            HostileUpdate();
        if (state == AIState.frenzy)
            FrenzyUpdate();
        if(state == AIState.dead)
            agent.enabled = false;
        if(state != AIState.dead)
            agent.enabled = true;
    }

    public virtual void Attack()
    {
        attackDelay = Time.time;    //resets attack delay
    }

    void FollowRoutes()
    {
        if(agent.enabled)
            agent.SetDestination(routes[currentRoute].position);                                    //set destination to current route on list

        float distance = Vector3.Distance(transform.position, routes[currentRoute].position);   //check distance between enemy and route
        if (distance < routeThreshold)                                                           //if close enough
        {
            currentRoute = nextRoute;                                                               //pick the next route
            nextRoute++;                                                                            //pick the next route
        }

        if (nextRoute > routes.Length)                                                           //if weve been to all routes
        {
            currentRoute = 0;                                                                       //reset
            nextRoute = 1;
        }
    }

    public virtual void TriggerIdle()
    {
        Debug.Log(name + " is in Idle.");
        state = AIState.idle;
    }

    void IdleUpdate()
    {
        FollowRoutes(); //Follow routes 

        if (Distance(player.position) <= suspiciousThreshold)   //if player gets too close, follow him
            TriggerSearching(player);
    }

    public virtual void TriggerSearching(Transform target)
    {
        focus = target;
        Debug.Log(name + " is Searching for " + focus.name);
        state = AIState.searching;
    }

    void SearchingUpdate()
    {
        if (agent.enabled)
            agent.SetDestination(focus.position);  //look for last known location

        if (Distance(focus.position) <= hostileThreshold)  //if we're close enough, start attacking
            TriggerHostile();

        if (Distance(focus.position) >= suspiciousThreshold)
            TriggerIdle();
    }

    public virtual void TriggerHostile()
    {
        Debug.Log(name + " is now Hostile.");
        state = AIState.hostile;
    }

    void HostileUpdate()
    {
        if (Distance(focus.position) <= attackThreshold)    //checks if target is within attack range
        {
            if(agent.enabled)
                agent.SetDestination(transform.position);   //stop moving

            Vector3 dir = new Vector3(focus.position.x, transform.position.y, focus.position.z);    //look at target
            transform.LookAt(dir);
        }
        else if(agent.enabled)
                agent.SetDestination(focus.position);   //follow target if it moves away
            
        if (Distance(focus.position) >= hostileThreshold) //if focus gets out of range return to searching
            TriggerSearching(focus);

        if (Time.time - attackDelay >= 1f / attackSpeed)    //handles attacks per second
            Attack();
    }

    public virtual void TriggerFrenzy()
    {
        Debug.Log(name + " is in a Frenzy.");
        frenzy = true;
        state = AIState.frenzy;
    }

    void FrenzyUpdate()
    {
        attackSpeed *= 1.5f;
        agent.speed *= 1.5f;

        if (focus != null)
            HostileUpdate();

        if (Distance(player.position) <= suspiciousThreshold)   //if player gets too close, follow him
            TriggerSearching(player);
    }

    public virtual void TriggerDead()
    {
        Debug.Log(name + " is dead.");
        state = AIState.dead;
        dead = true;
    }

    public virtual void Doot(Vector3 colliderPoint, float dootForce)
    {
        agent.enabled = false;
        dooted = true;
        Debug.Log(name + " has been Dooted.");
        rb.AddForce(colliderPoint * dootForce);
        StartCoroutine(DootRecover());
    }
    
    public virtual IEnumerator DootRecover()
    {
        yield return new WaitForSeconds(2);
        rb.velocity = Vector3.zero;
        dooted = false;
    }

    float Distance(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        return distance;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float currentHealth, maxHealth;
    public float speed;
    public float attackSpeed, attackDelay;
    Transform player;
    Transform focus;
    public float suspiciousThreshold, hostileThreshold, attackThreshold;

    #region Patrol
    NavMeshAgent agent;
    public Transform[] routes;
    public int currentRoute, nextRoute;
    public float routeThreshold;
    #endregion

    public enum AIState { idle, searching, hostile, frenzy, dead }
    public AIState state;

    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
        state = AIState.idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        StateCheck();
    }



    void StateCheck()
    {
        if (state == AIState.idle)
        {
            FollowRoutes(); //Follow routes 

            if (Distance(player.position) <= suspiciousThreshold)   //if player gets too close, follow him
                Searching(player);
        }

        if (state == AIState.searching)
        {
            agent.SetDestination(focus.position);  //look for last known location

            if (Distance(focus.position) <= hostileThreshold)  //if we're close enough, start attacking
                Hostile();

            if (Distance(focus.position) >= suspiciousThreshold)
                Idle();
        }

        if(state == AIState.hostile)
        {
            HostileUpdate();
        }

        if(state == AIState.frenzy)
        {

        }

        if(state == AIState.dead)
        {

        }
    }


    public virtual void Attack()
    {
        Debug.Log("attacked");
    }

    void FollowRoutes()
    {
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

    public virtual void Idle()
    {
        Debug.Log(name + " is in Idle.");
        state = AIState.idle;
    }

    public virtual void Searching(Transform target)
    {
        focus = target;
        Debug.Log(name + " is Searching for " + focus.name);
        state = AIState.searching;
    }

    public virtual void Hostile()
    {
        Debug.Log(name + " is now Hostile.");
        state = AIState.hostile;
    }

    void HostileUpdate()
    {
        if (Distance(focus.position) <= attackThreshold)    //checks if target is within attack range
        {
            agent.SetDestination(transform.position);   //stop moving

            Vector3 dir = new Vector3(focus.position.x, transform.position.y, focus.position.z);    //look at target
            transform.LookAt(dir);

        }
        else
            agent.SetDestination(focus.position);   //follow target if it moves away

        if (Distance(focus.position) >= hostileThreshold) //if focus gets out of range return to searching
            Searching(focus);

        if (Time.time - attackDelay >= 1f / attackSpeed)    //handles attacks per second
        {
            Attack();
            attackDelay = Time.time;
        }
    }

    public virtual void Frenzy()
    {
        Debug.Log(name + " is in a Frenzy.");
        state = AIState.frenzy;
    }

    public virtual void Dead()
    {
        Debug.Log(name + " is dead.");
        state = AIState.dead;
    }



    public virtual void Doot()
    {
        Debug.Log(name + " has been Dooted.");
    }

    float Distance(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        return distance;
    }
}

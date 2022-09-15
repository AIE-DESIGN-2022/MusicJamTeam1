using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This static helper class is designed to help
/// with some of the transform navigation and will
/// be added to as neccessity requires it.
/// </summary>
public static class TransformNavigation
{
    /// <summary>
    /// This GetAllChildren function calls a recursive private function under the same name
    /// and finds all the children inside that belong to a top level parent
    /// of transforms
    /// </summary>
    /// <param name="_topLevelParent"> This paramter should be the top level transform
    /// and should be your starting point for the search</param>
    /// <param name="_isChildActive"> This boolean allows you to do a search for just active children </param>
    /// /// <param name="_includeParent"> This boolean allows to select whether you want to include the parent inside 
    /// the list </param>
    /// <returns> The return will be the list of children which can be used as pleased </returns>
    public static List<Transform> GetAllChildren(Transform _topLevelParent, bool _isChildActive = true, bool _includeParent = false)
    {
        // Begin the recursive function and feed in the list to be populated
        List<Transform> finalList = new List<Transform>();
        GetAllChildren(_topLevelParent, finalList, _isChildActive);

        // quickly check whether the parent should be added and
        // if so, then add them
        if (_includeParent)
        {
            finalList.Add(_topLevelParent);
        }

        // return after all the recursive function is complete
        return finalList;
    }

    /// <summary>
    /// This is a recursive private function that calls it self and adds elements 
    /// to a given list until there are no children left to find
    /// </summary>
    /// <param name="_parent"> The parent is the higher level transform in the hierachy </param>
    /// <param name="_list"> The list being manipulated and having values added</param>
    /// <param name="_isChildActive"> a boolean fed in that informs the function whether to add active gameobjects or not </param>
    private static void GetAllChildren(Transform _parent, List<Transform> _list, bool _isChildActive)
    {
        foreach (Transform child in _parent)
        {
            if (_isChildActive)
            {
                if (child.gameObject.activeSelf)
                {
                    _list.Add(child);
                }
            }
            else
            {
                _list.Add(child);
            }

            GetAllChildren(child, _list, _isChildActive);
        }
    }

}

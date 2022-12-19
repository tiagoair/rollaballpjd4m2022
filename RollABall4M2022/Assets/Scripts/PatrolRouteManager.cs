using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRouteManager : MonoBehaviour
{
    public List<Transform> patrolPoints;

    private void Awake()
    {
        patrolPoints = new List<Transform>();
        
        Transform[] children = GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < children.Length; i++)
        {
            patrolPoints.Add(children[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < children.Length; i++)
        {
            if (i == children.Length - 1)
            {
                Debug.DrawLine(children[i].position, children[1].position);
            }
            else
            {
                Debug.DrawLine(children[i].position, children[i+1].position);
            }
        }
    }
}

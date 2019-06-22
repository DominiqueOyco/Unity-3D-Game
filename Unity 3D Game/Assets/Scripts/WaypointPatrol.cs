using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; //references each part of the transform component
    int m_CurrentWaypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        //check if navmeshcheck agent arrived at its destination
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length; //if currentwaypointindex + 1 = #elements in waypoints, set it to 0.
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); //set the destination of the ghost to whatever waypoint it is currently at
        }
    }
}
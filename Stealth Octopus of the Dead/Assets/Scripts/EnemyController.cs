using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent NavAgent;
    public GameObject[] Waypoints;

    private int nextWaypoint;
    private int iterator;  

	// Use this for initialization
	void Start () {
        NavAgent = GetComponent<NavMeshAgent>();
        nextWaypoint = 0;
        iterator = 1; 
	}
	
	// Update is called once per frame
	void Update () {
       int waypointCount =  Waypoints.GetLength(0);

        //If the navAgent doesn't have a path, send it towards the next waypoint
        if (!NavAgent.hasPath)
        {
            NavAgent.destination = Waypoints[nextWaypoint].transform.position;

            //update the nextWaypoint variable
            nextWaypoint += iterator;
            //see if the iterator needs to be changed
            if (nextWaypoint == 0 || nextWaypoint == (waypointCount - 1))
                iterator *= -1; 
        }

	}
}

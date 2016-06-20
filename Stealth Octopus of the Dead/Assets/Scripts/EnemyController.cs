using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent NavAgent;
    public GameObject[] Waypoints;
    public GameObject CurrentWaypoint; 

    private double waitDuration; 
    private int nextWaypoint;
    private bool hasWaited; 
    private int iterator;  

	// Use this for initialization
	void Start () {
        NavAgent = GetComponent<NavMeshAgent>();
        waitDuration = 0.0f; 
        nextWaypoint = 0;
        iterator = 1;
        CurrentWaypoint = Waypoints[0];
        hasWaited = true;  
	}
	
	// Update is called once per frame
	void Update () {

       int waypointCount =  Waypoints.GetLength(0);

        //see if the agent is done waiting
        if (!hasWaited && !NavAgent.hasPath)
        {
            waitDuration -= Time.deltaTime;
            if (waitDuration <= 0.0f)
                hasWaited = true; 
        }
        else
        {
            //If the navAgent doesn't have a path, send it towards the next waypoint
            if (!NavAgent.hasPath)
            {
                //First let's reset our waiting stuff
                hasWaited = false;
                waitDuration = Waypoints[nextWaypoint].GetComponent<Waypoint>().StayDuration;

                //set the destination
                NavAgent.destination = Waypoints[nextWaypoint].transform.position;

                //update the nextWaypoint variable
                nextWaypoint += iterator;
                //see if the iterator needs to be changed
                if (nextWaypoint == 0 || nextWaypoint == (waypointCount - 1))
                    iterator *= -1;
            }
        }
	}
}

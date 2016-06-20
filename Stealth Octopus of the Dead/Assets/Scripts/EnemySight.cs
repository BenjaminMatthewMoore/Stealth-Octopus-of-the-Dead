using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    //===============================
    //Vartiable Delcarations 

    public int scenetoload; 
    public bool PlayerInSight;
    public Vector3 lastKnownLocation;

    private NavMeshAgent navAgent;
    private Light Spotlight;
    private GameObject Player;
    private GameEnder endgame; 
    

	// Use this for initialization
	void Start () {
        //First find our references 
        Player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        Spotlight = gameObject.GetComponentInChildren<Light>();
        endgame = gameObject.GetComponent<GameEnder>(); 

	}
	
	// Update is called once per frame
	void Update () {

        //See if the player is within the view of the spotlight. 
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, Spotlight.transform.forward); 

        if (angle < Spotlight.spotAngle /2 )
        {
            //player is in sight so end the level. 
            endgame.EndLevel(); 
        }


	}
}

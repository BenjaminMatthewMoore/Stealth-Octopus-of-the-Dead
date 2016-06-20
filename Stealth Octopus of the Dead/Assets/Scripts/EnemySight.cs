using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    //===============================
    //Vartiable Delcarations 

    //==============================
    //References to components and objects 
    private Light Spotlight;
    private GameObject Player;
    private GameEnder endgame;
    //=============================

    //=============================
    //General Variables
    public bool PlayerInSight;
    public double CountdownDuration; 
    
    private double CountdownRemaining; 
    //=============================


    // Use this for initialization
    void Start () {
        //First find our references 
        Player = GameObject.FindGameObjectWithTag("Player");
        Spotlight = gameObject.GetComponentInChildren<Light>();
        endgame = gameObject.GetComponent<GameEnder>();

        //Initialise any variables that require a default; 
        PlayerInSight = false;
        CountdownRemaining = 0.0f; 
	}
	
	// Update is called once per frame
	void Update () {

        //See if the player is within the view and range of the spotlight. 
        //Angle: 
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, Spotlight.transform.forward);
        //Range: 
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (angle < Spotlight.spotAngle / 2 && Spotlight.enabled && distance <= Spotlight.range)
        {
            //player is in sight, now make sure no objects are blocking the view by using a raycast
            direction = Player.transform.position - transform.position;
            direction.Normalize();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit) && hit.collider.gameObject.tag == "Player")
            {
                PlayerInSight = true;
            }
            else PlayerInSight = false; 
        }
        else PlayerInSight = false;

        //If the cooldown is above 0, we need to count it down. 
        if (CountdownRemaining > 0.0f)
            CountdownRemaining -= Time.deltaTime; 
        else if (Spotlight.enabled == false) //but if the countdown is over, and the light is still turned off we need to turn it back on
        {
            Spotlight.enabled = true; 
        }

        //if the player is in sight end the level. 
        if (PlayerInSight)
        {
            endgame.EndLevel(); 
        }

	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerProjectile")
        {
            //if the player shoots and hits the enemy, disable their light and start a countdown 
            Spotlight.enabled = false;
            CountdownRemaining = CountdownDuration; 
        }
    }
}

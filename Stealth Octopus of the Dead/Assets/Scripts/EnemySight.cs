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
    [Tooltip("Whether or not the player is in sight")]
    public bool PlayerInSight;
    [Tooltip("Whether or not the play is detected")]
    public bool PlayerDetected; 
    [Tooltip("How long the Enemy's light will be disabled when hit")]
    public double CountdownDuration;
    [Tooltip("How long in seconds it will take the enemy to detect the player")]
    public double DetectionTime;
    private double DetectionLevel;
    [Tooltip("How quickly the enemy's detection level will be reduced")]
    public float SightCooldownSpeed;
    [Tooltip("% of the lights range that the enemy can detect upto: example: if this set to 75, and the lights range is 10m, the enemy can detect upto 7.5m away")]
    public float DetectionRange; 
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
        PlayerDetected = false; 
        DetectionLevel = 0.0f; 
        CountdownRemaining = 0.0f; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        //See if the player is within the view and range of the spotlight. 
        //Angle: 
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, Spotlight.transform.forward);
        //Range: 
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (angle < Spotlight.spotAngle / 2 && Spotlight.enabled && distance <= Spotlight.range * (DetectionRange * 0.01))
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

        //if the player is in sight add to detection level 
        if (PlayerInSight)
        {
            DetectionLevel += Time.deltaTime;
            if (DetectionLevel >= DetectionTime)
                PlayerDetected = true; 
        }
        //if the player isn't in sight take away from detection level
        if (!PlayerInSight)
        {
            DetectionLevel -= Time.deltaTime * SightCooldownSpeed;
            if (DetectionLevel <= 0.0f)
                DetectionLevel = 0.0f; 
        }
        //if the player has been detected end the level 
        if (PlayerDetected)
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

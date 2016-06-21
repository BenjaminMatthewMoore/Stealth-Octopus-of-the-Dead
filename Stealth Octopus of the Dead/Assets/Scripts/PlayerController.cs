using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //cache the rigid body
    public Rigidbody rb;

    //=====================
    //Varaibles for shooting
    [Tooltip("This should be a prefab of the object the player will shoot")]
    public GameObject ProjectilePrefab;
    [Tooltip("This is the time in seconds between shots")]
    public double RateOfFire;
    //This is how long it has been since the last shot was fired
    private double timeSinceShot = 0.0f;
    [Tooltip("The maximum number of projectiles that can exist at once")]
    public int ProjectilePoolSize ;
    private GameObject[] Projectiles; 
    //=====================

    [Tooltip("Velocity added to the player x axis")]
    public float speed;

    [Tooltip("Velocity added to the player y axis")]
    public float jumpHeight; // Height at which the player Jumps.

    [Tooltip("Restricts x movement while in the air, 1 is free movemont 0 is none")]
    [Range(0,1)]
    public float jumpDrag; // The amount of side to side drag acted on the player while they are jumping.
                           // Allows player movement whilst in air, but also means the player stays relatively
                           // close to the point at which they jumped.

    public bool isGrounded; // Returns true if the player is on the ground. Allows no double air jumps.

    private float jumpTimer;

    private Vector3 vMovement; // Verical Movement.
    private Vector3 hMovement; // Horizontal movement.
    private Vector3 lastFrameVelocity;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;

        //Create the pool of projectiles 
        Projectiles = new GameObject[ProjectilePoolSize];
        Vector3 pos = new Vector3(-100, 0, 0); 
        for (int i = 0; i < ProjectilePoolSize; i++)
        {
            Projectiles[i] = (GameObject)Instantiate(ProjectilePrefab, pos, Quaternion.identity); 
        }
    }

    // Called before physics...
    void FixedUpdate()
    { 
        if(rb.velocity.y == lastFrameVelocity.y)
        {
            isGrounded = true;
        }
            // Handles horizontal movement.
            hMovement = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, 0f);
            vMovement = new Vector3(0f, Input.GetAxis("Jump") * jumpHeight, 0f);

            hMovement *= speed * Time.deltaTime;
            vMovement *= jumpHeight * Time.deltaTime;
            if (hMovement.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                if (isGrounded == true)
                {
                    rb.velocity = new Vector3(hMovement.x, rb.velocity.y, 0f);
                }
            }
            else if (hMovement.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                if (isGrounded == true)
                {
                    rb.velocity = new Vector3(hMovement.x, rb.velocity.y, 0f);
                }
                else
                {
                    {
                        hMovement *= jumpDrag;
                        rb.velocity = new Vector3(hMovement.x, rb.velocity.y, 0f);
                    }
                }
            }
        // Jumping
        if (Input.GetKey(KeyCode.W) && isGrounded == true || Input.GetButton("Jump") && isGrounded == true)
        {
            rb.velocity = new Vector3(hMovement.x, vMovement.y, 0f);
        }
        lastFrameVelocity = rb.velocity;
        //Shooting
        //Add time to the timeSinceShot variable 
        timeSinceShot += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeSinceShot >= RateOfFire)
        {
            timeSinceShot = 0.0f;
            GameObject projectile; 
            //Get an inActive projectile and shoot it
            for (int i = 0; i < ProjectilePoolSize; i++)
            {
                projectile = Projectiles[i];
                Debug.Log("almost"); 
                if (!projectile.GetComponent<Projectile>().GetActive())
                {
                    Debug.Log("Boom"); 
                    projectile.GetComponent<Projectile>().Activate(transform.position, transform.forward);
                    timeSinceShot = 0.0f;
                    break; 
                }
            }
        }
    }
}

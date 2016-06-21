using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    bool isActive;
    [Tooltip("How fast the projectile moves through the world")]
    public float speed;
    //The target the object will move towards
    private Vector3 direction; 
	// Use this for initialization
	void Start () {
        isActive = false; 
	}
	
	// Update is called once per frame
	void Update () {
        //only update if active
	    if (isActive)
        {
            //just move the projectile forward 
            Vector3 position = transform.position;
            position += direction * speed * Time.deltaTime;
            transform.position = position; 
        }
	}


    public bool GetActive()
    {
        return isActive; 
    }

    public void Activate(Vector3 a_position, Vector3 a_target)
    {
        Debug.Log("activing"); 
        transform.position = a_position;
        direction = a_target; 
        isActive = true; 
    }

    public void Deactivate(Vector3 reserveLocation)
    {
        isActive = false;
        transform.position = reserveLocation; 
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player")
        {
            Vector3 vec = new Vector3(-100, 0, 0);
            Deactivate(vec);
        }
    }
}

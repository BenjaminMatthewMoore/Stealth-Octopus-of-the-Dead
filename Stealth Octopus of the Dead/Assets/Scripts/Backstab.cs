using UnityEngine;
using System.Collections;

public class Backstab : MonoBehaviour
{

    private enum ActiveState { BlinkTo, BlinkBack };
    private ActiveState activeState;
    [Range(0,0.5f)]
    public float attackBlinkDelay;
    public float AttackRange;
    private Vector3 startPos;
    bool attacking;

    int closestIndex;
    public GameObject closestEnemy;
    // Use this for initialization

    void Start()
    {
        startPos = this.transform.position;
        attacking = false;
        closestEnemy = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        findClosestEnemy();
        if (Input.GetKey(KeyCode.E) && closestEnemy != null && !attacking)
        {
            startPos = this.transform.position;
            {
                Vector3 tempDist = this.transform.position - closestEnemy.transform.position;
                float dist = tempDist.magnitude;

                if (dist <= AttackRange)
                {
                    StartCoroutine(DoActivateCoroutine());
                    closestEnemy = null;
                }
            }
        }
    }


    void findClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, AttackRange);
        int i = 0;
        bool foundEnemy = false;
        float distance = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag != "Enemy")
            {
                i++;
                break;
            }
            foundEnemy = true;
            Vector3 tempDist = this.transform.position - hitColliders[i].transform.position;
            float dist = tempDist.magnitude;
            if (dist < distance || distance == 0)
            {
                distance = dist;
                closestIndex = i;
            }
            i++;
        }
        if (foundEnemy)
            closestEnemy = hitColliders[closestIndex].gameObject;
    }






    IEnumerator DoActivateCoroutine()

    {
        attacking = true;
        activeState = ActiveState.BlinkTo;
        this.transform.position = closestEnemy.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().Sleep();
        DestroyObject(closestEnemy);
        yield return new WaitForSeconds(attackBlinkDelay);
        attacking = false;
        activeState = ActiveState.BlinkBack;
        this.transform.position = startPos;
    }
}
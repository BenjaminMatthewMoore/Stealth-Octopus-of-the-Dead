﻿using UnityEngine;
using System.Collections;

public class Backstab : MonoBehaviour
{

    private enum ActiveState { BlinkTo, BlinkBack };
    private ActiveState activeState;
    [Range(0, 0.5f)]
    public float attackBlinkDelay;
    [Range(0, 1.0f)]
    public float delayAttack;
    public float AttackRange;
    private Vector3 startPos;
    public float closestDistance;
    public bool attacking;
    public GameObject[] enemyList;
    public int closestIndex;
    public GameObject closestEnemy;
    public int killScore;
    ParticleSystem smokeSystem;
    // Use this for initialization

    void Start()
    {
        startPos = this.transform.position;
        attacking = false;
        closestEnemy = null;
        killScore = 0;
        smokeSystem = GetComponentInChildren<ParticleSystem>();
        smokeSystem.Stop();
        smokeSystem.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        findClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 temp = this.transform.position - closestEnemy.transform.position;
            float closestDistance = temp.magnitude;
        }
        if (Input.GetKey(KeyCode.E) && closestEnemy != null && !attacking)
        {
            startPos = this.transform.position;
            {

                Vector3 tempDist = this.transform.position - closestEnemy.transform.position;
                float dist = tempDist.magnitude;
                closestDistance = dist;
                if (closestDistance <= AttackRange)
                {
                    StartCoroutine(DoActivateCoroutine());
                }
            }
        }
    }


    void findClosestEnemy()
    {
      enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        float distance = 0;
        while (i < enemyList.Length)
        {
            Vector3 tempDist = this.transform.position - enemyList[i].transform.position;
            float dist = tempDist.magnitude;
            if (dist <= distance || distance == 0)
            {
                distance = dist;
                closestIndex = i;
            }
            i++;
        }
        if(enemyList.Length > 0)
            closestEnemy = enemyList[closestIndex].gameObject;
    }






    IEnumerator DoActivateCoroutine()

    {
        attacking = true;
        smokeSystem.Play();
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().Sleep();
        yield return new WaitForSeconds(delayAttack);
        this.transform.position = closestEnemy.transform.position;
        DestroyObject(closestEnemy);
        yield return new WaitForSeconds(attackBlinkDelay);
        smokeSystem.Clear();
        smokeSystem.Stop(); Debug.Log("RTUN");
        attacking = false;
        activeState = ActiveState.BlinkBack;
        this.transform.position = startPos;

        killScore++;
        closestEnemy = null;
    }
}
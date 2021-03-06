﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public bool isLightHit = false;
    public bool isHeavyHit = false;
    public float enemyLightAtkKnockBackTime = 0.2f;
    public float enemyHeavyAtkKnockBackTime = 0.4f;
    public float heavyAtkDmgCoolDown = 0f;
    private bool isOnCombat = false;

    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(isLightHit == true)
        {
            LightKnockBackEnemy(gameObject);
        }
        if (isHeavyHit == true)
        {
            HeavyKnockBackEnemy(gameObject);  
        }
        if (heavyAtkDmgCoolDown > 0)
        {
            heavyAtkDmgCoolDown -= Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit Enemy");  
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Enemy")
        {
            if (PlayerBehaviour.isLightHit == true)
            {
                isLightHit = true;
                isOnCombat = true;
                //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(-other.transform.forward.normalized), 1f);
                Debug.Log("knock back");
            }

            if (PlayerBehaviour.isHeavyHit == true)
            {
                isHeavyHit = true;
                isOnCombat = true;
                //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(-other.transform.forward.normalized), 1f);

                Debug.Log("knock back");
            }

            Debug.Log(other.ToString());
            Debug.Log("Hit Enemy");
            
        }
    }

    void LightKnockBackEnemy(GameObject enemy)
    {
        float Velocity = 0.1f;
        if (enemyLightAtkKnockBackTime > 0 && isLightHit == true)
        {
            enemyLightAtkKnockBackTime -= Time.deltaTime;
            
            Vector3 knockBackVector = (GameObject.Find("Player").transform.forward * Velocity).normalized;
            enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
        }
        if(isOnCombat == true)
        {
            player.GetComponent<SwordCombat>().resetOutOfCombatTime = player.GetComponent<SwordCombat>().setOutOfCombatTime();
            player.GetComponent<SwordCombat>().isOnCombat = true;
            enemy.GetComponent<Enemy>().HP -= 30;
            isOnCombat = false;
        }

        if(enemyLightAtkKnockBackTime <= 0)
        {
            enemyLightAtkKnockBackTime = 0.2f;
            isLightHit = false;
        }
    }

    void HeavyKnockBackEnemy(GameObject enemy)
    {
        float Velocity = 0.2f;
        if (enemyHeavyAtkKnockBackTime > 0 && isHeavyHit == true)
        {
            enemyHeavyAtkKnockBackTime -= Time.deltaTime;
            Vector3 knockBackVector = (GameObject.Find("Player").transform.forward * Velocity).normalized;
            enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
        }

        if (isOnCombat == true && heavyAtkDmgCoolDown <= 0)
        {
            heavyAtkDmgCoolDown = setHeavyAtkCoolDown();
            player.GetComponent<SwordCombat>().resetOutOfCombatTime = player.GetComponent<SwordCombat>().setOutOfCombatTime();
            player.GetComponent<SwordCombat>().isOnCombat = true;
            enemy.GetComponent<Enemy>().HP -= 50;
            isOnCombat = false; 
        }

        if (enemyHeavyAtkKnockBackTime <= 0)
        {
            enemyHeavyAtkKnockBackTime = 0.2f;
            isHeavyHit = false;
        }
    }

    float setHeavyAtkCoolDown()
    {
        return 1.0f;
    }
}


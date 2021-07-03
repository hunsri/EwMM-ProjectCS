using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    private EnemyHealth _health;
    private int _spreadingRate;
    private NPC.Behaviors _behavior = NPC.Behaviors.UNINFECTED;

    void OnCollisionEnter(Collision collision){
        GameObject go = collision.gameObject;

        if(go.tag == "Enemy"){
            if(_behavior == NPC.Behaviors.INFECTED){
                _health.HealthImproved(_spreadingRate);
            }
        }
    }

}
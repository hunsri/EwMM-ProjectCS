using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC;

/**
* Add to player 
* Need to add EnemyHealth script to the player for this to work
**/
public class PlayerHealth : MonoBehaviour
{
    /* the player */
    [SerializeField]
    private GameObject player;
    /*
    [SerializeField]
    private bool _isFFP = false; //if player is wearing a ffp2 mask, the risk lowers?
    */

    /* player health displayed on the screen */
    private EnemyHealth _health; 

    /* infection risk percentage */
    private int _spreadingPercent;

    /* infection "damange" of the player */
    private const int _infection = 10;

    void Start(){
        //connect healthbar and it's functions to the player
        _health = player.GetComponent<EnemyHealth>();
        // TODO: Delete later, once masktypes are implemented
        // just for testing
        _spreadingPercent = 90;
    }

    /**
    * When an infected NPC collides with the Player body. The health of the player drops
    **/
    void OnCollisionEnter(Collision collision){  

        //the npc 
        GameObject go = collision.gameObject;
        // status of the npc (infected, uninfected, cured)
        Behaviors behaviors = go.GetComponent<NPCBehavior>().GetBehaviors();
        Debug.Log("Collided with: " + behaviors);

        // if npc wears mask, get the assigned spreading percent
        // MaskType mask = go.GetComponent<MaskType>().GetMaskType();

        //if an npc is infected 
        if(behaviors == Behaviors.INFECTED){

            // randomize probabilty of getting infected
            int probability = UnityEngine.Random.Range(1, 100);
            
            // TODO repalace if masktypes get implemented
            //if(probability < mask)
            if(probability < _spreadingPercent){
                //drop in "health" (healthbar gets filled because infection risk increased)
                _health.HealthImproved(_infection);
            }

        }
    }

    //TODO: Delete if use enum masktype has int constants, use if masktype has no int constants
    /*
    * Method to assign the spreading percent of the different mask type
    * /
    void InfectionRate(MaskType type){
        switch(type){
            case MaskType.NONE:
                _spreadingPercent = 70;
                break;
            case MaskType.NORMAL:
                _spreadingPercent = 50;
                break;
            case MaskType.FFP:
                _spreadingPercent = 15;
                break;
        }
    } 
    */
}
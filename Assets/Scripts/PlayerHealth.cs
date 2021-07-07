using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC;

using Data;

/**
* Add to player 
* Need to add EnemyHealth script to the player for this to work
**/
public class PlayerHealth : MonoBehaviour
{
    /* the player */
    [SerializeField]
    private GameObject player;

    /* player health displayed on the screen */
    private EnemyHealth _health;

    /* infection risk percentage */
    private int _spreadingPercent;

    /* infection "damage" of the player */
    private const int _infection = 10;

    //represents if the player has the improved mask or not
    private bool _hasFFP2;

    void Start()
    {
        //determines whether the player has the improved mask or not
        _hasFFP2 = new PlayerStats().GetHasMaskReward();

        //connect healthbar and its functions to the player
        _health = player.GetComponent<EnemyHealth>();

        //setting the probability of loosing health
        if(_hasFFP2)
        {   
            //with improved mask
            _spreadingPercent = 30;
        }
        else
        {   
            //default
            _spreadingPercent = 60;
        }
    }

    /**
    * When an infected NPC collides with the Player body. The health of the player drops
    **/
    void OnCollisionEnter(Collision collision)
    {

        //the npc 
        GameObject go = collision.gameObject;
        // status of the npc (infected, uninfected, cured)
        go.TryGetComponent<NPCBehavior>(out NPCBehavior nPCBehavior);
        if (nPCBehavior)
        {
            Behaviors behaviors = nPCBehavior.GetBehaviors();
            Debug.Log("Collided with: " + behaviors);
            
            // if npc wears mask, get the assigned spreading percent
            // MaskType mask = go.GetComponent<MaskType>().GetMaskType();
            //InfectionRate(mask);

            //if an npc is infected 
            if (behaviors == Behaviors.INFECTED)
            {
                // randomize probabilty of getting infected
                int probability = UnityEngine.Random.Range(1, 100);

                if (probability < _spreadingPercent)
                {
                    //drop in "health" (healthbar gets filled because infection risk increased)
                    //>>this is kinda confusing, but apparently the "improvement" is that the infection bar gets filled
                    _health.HealthImproved(_infection);
                }
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

    public float getHealth(){
        float currentHealth = _health.healthbar.fillAmount;
        //Debug.Log("Player Health:" + currentHealth);
        return currentHealth;
    }
}
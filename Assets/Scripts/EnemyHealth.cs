using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Skript für die Healthbar von den NPCs.  */
public class EnemyHealth : MonoBehaviour
{
    public Image healthbar;

    private float _startHealth = 100;
    //current health
    private float _health;
    void Start()
    {
        _health = _startHealth;

        /*
        if(Enemy.Infected){ 
            _health = 0;
        } else {
            _health = _startHealth;
        } */
    }

    // fillAmount: float between  0 and 1
    // 0 = all red
    // 1 = all green
    public void HealthImproved(float amount)
    {
        _health += amount;
        healthbar.fillAmount = _health / _startHealth;
    }

    public void HealthDecreased(float amount)
    {
        _health -= amount;
        healthbar.fillAmount = _health / _startHealth;
    }

}

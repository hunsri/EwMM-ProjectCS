using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Skript für die Healthbar von den NPCs.  */
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool _infected = false;
    public Image healthbar;

    private float _startHealth = 100;
    //current health
    private float _health;


    void Start()
    {
        if (_infected)
        {
            _health = 0;
            UpdateHealthBar();
            return;
        }

        _health = _startHealth;
    }

    // fillAmount: float between  0 and 1
    // 0 = all red for npc, transparent for player
    // 1 = all green for npc, red for player

    /* Fills up the healthbar with colour 
    Use when:
    - player health decreases (red)
    - npc health improves (green) */
    public void HealthImproved(float amount)
    {
        _health += amount;
        UpdateHealthBar();
    }

    /* 'takes' colour FROM the healthbar
    Use when:
    - player health improves (transparent)
    - npc health decreases (red)
    */
    public void HealthDecreased(float amount)
    {
        _health -= amount;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthbar.fillAmount = _health / _startHealth;
    }

}

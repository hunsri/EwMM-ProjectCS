using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage = 25;

    private readonly string[] _tags = { "Player", "Weapon", "Projectile" };
    private bool _collided;

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.transform.tag;
        if (Array.IndexOf(_tags, tag) == -1 && !_collided)
        {
            _collided = true;
            Destroy(gameObject);

            if (tag == "Target")
            {
                SoundManager.soundManager.PlayGotTarget(collision.transform.position);
                TargetController target = collision.transform.GetComponent<TargetController>();
                target.ReduceHitpoint(_damage);
            }

            if (tag == "Enemy")
            {
                SoundManager.soundManager.PlayGotTarget(collision.transform.position);
                EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
                enemyHealth.HealthImproved(_damage);
            }

            if (tag == "Save")
            {
                FindObjectOfType<PlayerDataManager>().SaveGame();
            }

            if (tag == "Load")
            {
                FindObjectOfType<PlayerDataManager>().LoadGame(true);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
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
                TargetController target = collision.transform.GetComponent<TargetController>();
                target.ReduceHitpoint(_damage);
            }
        }
    }
}

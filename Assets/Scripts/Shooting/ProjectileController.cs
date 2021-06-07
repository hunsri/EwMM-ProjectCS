using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private readonly string[] tags = { "Player", "Weapon", "Projectile" };
    private bool collided;
    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.transform.tag;
        if (Array.IndexOf(tags, tag) == -1 && !collided)
        {
            collided = true;
            Destroy(gameObject);

            if (tag == "Target")
            {
                TargetController target = collision.transform.GetComponent<TargetController>();
                target.ReduceHitpoint();
            }
        }
    }
}

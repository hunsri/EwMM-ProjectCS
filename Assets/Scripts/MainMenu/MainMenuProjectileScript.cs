using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuProjectileScript : MonoBehaviour
    {
        private readonly string[] tags = { "Player", "Weapon", "Projectile" };
        private bool collided;

        // Let an object with the tag "target" react to collision
        void OnCollisionEnter(Collision collision)
        {
            string tag = collision.transform.tag;
            if (Array.IndexOf(tags, tag) == -1 && !collided)
            {
                collided = true;
                Destroy(gameObject);

                if (tag == "Target")
                {
                    if (collision.transform.GetComponent<SliderSettingScript>())
                    {
                        SliderSettingScript target = collision.transform.GetComponent<SliderSettingScript>();
                        target.TargetReact();
                    }
                    else if (collision.transform.GetComponent<DifficultySettingScript>())
                    {
                        DifficultySettingScript target = collision.transform.GetComponent<DifficultySettingScript>();
                        target.TargetReact();
                    }
                    else
                    {
                        Debug.Log("Something went bad on OnCollisionEnter() in MainMenuProjectileScript!");
                    }
                }
            }
        }
    }
}
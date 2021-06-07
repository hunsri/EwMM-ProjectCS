using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    private MeshRenderer renderer;
    private int health = 100;
    private int onHit = 25;
    private bool shouldUpdate = false;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdate)
        {
            string shaderPropertyName = "_Color";
            Color color = Color.white;
            switch (health)
            {
                case 100:
                    color = Color.white;
                    break;
                case 75:
                    color = Color.yellow;
                    break;
                case 50:
                    color = Color.Lerp(Color.yellow, Color.red, 0.5f);
                    break;
                case 25:
                    color = Color.red;
                    break;
                case 0:
                    DestroySelf();
                    return;
            }

            renderer.material.SetColor(shaderPropertyName, color);
            shouldUpdate = false;
        }

    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void ReduceHitpoint()
    {
        health -= onHit;
        shouldUpdate = true;
    }
}

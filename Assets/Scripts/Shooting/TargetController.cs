using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    private MeshRenderer _renderer;
    private int _health = 100;
    private bool _shouldUpdate = false;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldUpdate)
        {
            string shaderPropertyName = "_Color";
            Color color = Color.white;
            switch (_health)
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

            _renderer.material.SetColor(shaderPropertyName, color);
            _shouldUpdate = false;
        }

    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void ReduceHitpoint(int damage)
    {
        _health -= damage;
        _shouldUpdate = true;
    }
}

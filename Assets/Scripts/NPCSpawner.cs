using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _npc;
    [SerializeField]
    private int _amount;

    // private DateTime _lastSpawn;
    // private int _deltaMilliseconds = 500;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <_amount; i++) {

            Instantiate(_npc, this.transform.position, Quaternion.Euler(0,0,0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

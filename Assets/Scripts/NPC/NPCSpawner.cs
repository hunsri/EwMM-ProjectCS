using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace NPC
{
    public class NPCSpawner : MonoBehaviour
    {
        [SerializeField]
        private Behaviors _behavior;

        [SerializeField]
        private Transform _npc;
        [SerializeField]
        private int _amount;
        private int _amountSpawned;

        private DateTime _lastSpawn;
        private int _deltaMilliseconds = 500;
        // Start is called before the first frame update
        void Start()
        {
            _lastSpawn = System.DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            if(_amountSpawned < _amount)
            {
                if(_lastSpawn.AddMilliseconds(_deltaMilliseconds) < System.DateTime.Now)
                {   
                    GameObject go;

                    go = Instantiate(_npc, this.transform.position, Quaternion.Euler(0,0,0)).gameObject;
                    _lastSpawn = System.DateTime.Now;
                    _amountSpawned++;

                    
                    //setting the behavior of the spawned NPC
                    NPCBehavior script = go.GetComponent<NPCBehavior>();
                    script.ChangeBehavior(_behavior);
                }
            }
        }
    }
}

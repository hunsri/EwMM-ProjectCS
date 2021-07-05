using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace NPC
{
    ///<summary>
    /// This script is intended to be applied to a GameObject so that it can spawn a given NPC.
    /// This script requires that the SceneControllerObject is present in the scene which has <c>NPCWaveManager</c> attached to it
    ///</summary>
    public class NPCSpawner : MonoBehaviour
    {
        [SerializeField]
        private int _activeOnWave;
        //the script that holds the information about the waves e.g. what wave is currently active
        private NPCWaveManager _waveManager;

        [SerializeField]
        private Behaviors _behavior;

        [SerializeField]
        private Transform _npc;
        [SerializeField]
        private int _amount;

        [SerializeField]
        private bool _isNoVac = false;

        [SerializeField]
        private bool _isNoMask = false;

        [SerializeField]
        private int maskedValue = 1;

        private int _amountSpawned;

        private DateTime _lastSpawn;
        private int _deltaMilliseconds = 500;

        // Start is called before the first frame update
        void Start()
        {
            _waveManager = FindObjectOfType<NPCWaveManager>();

            if (_waveManager == null)
            {
                Debug.LogWarning("Couldn't find the NPCWaveManager! " +
                "Please make sure there is a SceneController Object with an attached NPCWaveManager script present!");
            }

            if (_activeOnWave < 1)
            {
                Debug.LogWarning("The Active On Wave value of " + this.name + " needs to be set to a value above 0 in order to work!");
            }

            //initializing _lastSpawn
            _lastSpawn = System.DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            if (_waveManager.CurrentWave == _activeOnWave && !_waveManager.IsPaused)
            {
                if (_amountSpawned < _amount)
                {
                    if (_lastSpawn.AddMilliseconds(_deltaMilliseconds) < System.DateTime.Now)
                    {
                        GameObject go;

                        go = Instantiate(_npc, this.transform.position, Quaternion.Euler(0, 0, 0)).gameObject;
                        // debug purposes. Spawn NPC with behaviors.
                        go.GetComponent<NPCBehavior>().SetBehaviors(_isNoMask, _isNoVac, maskedValue);
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
}

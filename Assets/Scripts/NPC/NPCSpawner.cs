using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
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
        private MaskType maskType = MaskType.NONE;

        private int _amountSpawned;

        private DateTime _lastSpawn;
        private int _deltaMilliseconds = 500;
        private bool _isSettingsAccessible = false;
        private PlayerDataManager _dataManager;
        private int _difficulty = 0; // in case there's an error while loading saved data


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
            StartCoroutine(GetDifficulty());
        }

        /// <summary>
        /// Get difficulty level from saved user settings.
        /// </summary>
        IEnumerator GetDifficulty()
        {
            Debug.Log("Loading user settings");
            yield return new WaitUntil(() => _isSettingsAccessible);
            Debug.Log("Loading is successful");
            _difficulty = _dataManager.GetDifficulty();
            Debug.Log("Difficulty: " + _difficulty);
        }

        NPCAttributes GetAttributes()
        {
            switch (_difficulty)
            {
                case 1:
                    return NPCDifficultyLevel.MediumNPCAttribute;
                case 2:
                    return NPCDifficultyLevel.HardNPCAttribute;
                case 0:
                default:
                    return NPCDifficultyLevel.EasyNPCAttribute;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_isSettingsAccessible)
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
                            go.GetComponent<NPCBehavior>().SetBehaviors(_isNoMask, _isNoVac, maskType);
                            _lastSpawn = System.DateTime.Now;
                            _amountSpawned++;


                            //setting the behavior of the spawned NPC
                            NPCBehavior script = go.GetComponent<NPCBehavior>();
                            script.ChangeBehavior(_behavior);
                            script.SetAttributes(GetAttributes());
                        }
                    }
                }
            }
            else
            {
                _dataManager = FindObjectOfType<PlayerDataManager>();
                if (_dataManager != null)
                {
                    _isSettingsAccessible = _dataManager.IsDataLoaded();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Powerup;

namespace NPC
{
    ///<summary>
    /// This script is intended to be applied to the SceneControllerObject.
    /// At least one needs to be present if a scene implements <c>NPCSpawner</c>
    ///</summary>
    public class NPCWaveManager : MonoBehaviour
    {
        [SerializeField]
        private int _maxWaves;

        public int CurrentWave { get; set; }
        public bool AllWavesOver { get; private set; }


        [SerializeField]
        private int _waveDurationSeconds;

        private DateTime _waveStart;
        private DateTime _waveEnd;
        private DateTime _timeSpendInPause;
        private DateTime _pauseStart;

        public GameObject newWavePanel;

        //holds if the wave is paused
        //ONLY ACCESS THIS FIELD THROUGH ITS PROPERTY - things will break if you ignore this
        private bool _isPaused;

        private PowerupManager _powerupManager;
        private bool _isDataLoaded = false;

        ///<summary>
        /// Property that exposes the paused status
        /// All changes or checks regarding that status HAVE to be made through this property
        ///</summary>
        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                //only change the status if it differs from current one
                if (value != _isPaused)
                    if (value)
                    {
                        //start the pause and save when it started
                        _pauseStart = DateTime.Now;
                        _isPaused = true;
                    }
                    else
                    {
                        //end the pause and apply the time that was spent in pause to the time when the wave ends
                        _waveEnd = _waveEnd.Add(DateTime.Now - _pauseStart);
                        _isPaused = false;
                    }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _powerupManager = GetComponent<PowerupManager>();
            CurrentWave = 0; // Initialized default value for current wave, otherwise the wave won't start at 1 on load game
        }

        void Update()
        {
            if (_isDataLoaded)
            {
                if (!IsPaused)
                {
                    //checking if the current wave is over yet
                    if (System.DateTime.Now > _waveEnd)
                    {
                        if (newWavePanel != null)
                            newWavePanel.SetActive(true);
                        SoundManager.soundManager.LevelSounds(2);
                        StartNewWave();
                    }
                }

                if (System.DateTime.Now > _waveStart.AddSeconds(3))
                {
                    if (newWavePanel != null)
                        newWavePanel.SetActive(false);
                }
            }
            else
            {
                _isDataLoaded = _powerupManager.IsLoaded();
            }
        }

        /// <summary>
        /// Needs to be called each time a new wave is started
        /// </summary>
        void StartNewWave()
        {
            if (!AllWavesOver && _isDataLoaded)
            {
                //check if the wave duration has been set to a valid value
                if (_waveDurationSeconds > 0)
                {
                    //check that we don't go above the maximum of waves
                    if (CurrentWave < _maxWaves)
                    {
                        CurrentWave++;
                        _waveStart = System.DateTime.Now;
                        _waveEnd = _waveStart.AddSeconds(_waveDurationSeconds);

                        Debug.Log("Wave " + CurrentWave + " started!");
                        _powerupManager.CheckForPowerups(); // apply powerups on start of every wave
                    }
                    else
                    {
                        AllWavesOver = true;
                        //Debug.Log("FINAL WAVE OVER!");
                    }
                }
                else
                {
                    Debug.LogWarning("Wave Duration hasn't been set or it is negative. Please enter a valid value above 0!");
                    //and no, 0! does not mean 1 xD
                }
            }
        }
    }
}

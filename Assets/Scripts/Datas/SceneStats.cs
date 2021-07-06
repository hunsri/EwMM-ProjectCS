using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using NPC;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

namespace Data
{   
    ///<summary>
    /// This class is used to interact with the savestate of a scene represented by <c>SceneStatsData</c>
    ///</summary>
    [System.Serializable]
    public class SceneStats : MonoBehaviour
    {   
        //the name of the save file
        public readonly string SaveFileName = "CoronaShotsSceneStats.data";
        //the entire path including the save file name
        private string _saveFilePath;
        private BinaryFormatter _formatter = new BinaryFormatter();

        private SceneStatsData _sceneStatsData;
        private NPCWaveManager _waveManager;

        public void Awake()
        {
            _saveFilePath = Application.persistentDataPath + "/" + SaveFileName;

            _waveManager = FindObjectOfType<NPCWaveManager>();

            if(_waveManager == null)
            {
                Debug.LogWarning("Couldn't find the NPCWaveManager! "+
                "Please make sure there is a SceneController Object with an attached NPCWaveManager script present!");
            }
            else
            {
                _sceneStatsData = ReloadSceneStats();
                
                //IF no save of the scene exists
                //OR the savestate is from another scene
                //THEN create a new SceneStatsData object 
                if(_sceneStatsData == null || SceneManager.GetActiveScene().name != _sceneStatsData.SceneName)
                {
                    _sceneStatsData = new SceneStatsData();
                    _sceneStatsData.SceneName = SceneManager.GetActiveScene().name;
                }
                else //in case a valid savestate for the current scene exists 
                {
                    //start the WaveManager at the saved time (the last, non completed wave)
                    //we need to start the wave that comes next since the one saved has already been through
                    _waveManager.CurrentWave = _sceneStatsData.ResultOfWaveNumber+1;
                }
            }
        }

        public void Update()
        {   
            //TODO change origin of call to begin in WaveManager
            if(_waveManager.AllWavesOver)
            {   
                //checking if the data has been sent already
                //a non existent file indicates that the data has been sent already
                if (File.Exists(_saveFilePath))
                    TransferScoreToPlayerStats();
            }
            //if the data from the end of the wave before the current one is more than one wave ago, then save;
            //this marks the time a new wave has started and the data from the wave before needs to be saved 
            else if (_sceneStatsData.ResultOfWaveNumber < _waveManager.CurrentWave-1)
            {
                SaveSceneStats();

                //the result of the wave that is over gets saved, not the current one that is still active
                _sceneStatsData.ResultOfWaveNumber = _waveManager.CurrentWave-1;

                Debug.Log("Saved result of wave "+ _sceneStatsData.ResultOfWaveNumber);
            }
        }

        public void IncrementCuredNPCS()
        {
            _sceneStatsData.CuredNPCS++;
            _sceneStatsData.Score += 120;
            //Debug.Log("cured: "+ _sceneStatsData.CuredNPCS);
        }

        public void IncrementVaccinesShot()
        {
            _sceneStatsData.VaccinesShot++;
            _sceneStatsData.Score -= 20;
            //Debug.Log("vaccines shot: "+ _sceneStatsData.VaccinesShot);
        }

        public void IncrementMasksShot()
        {
            _sceneStatsData.MasksShot++;
            //Debug.Log("masks shot: "+ _sceneStatsData.MasksShot);
        }

        public void IncrementInfectionEvents()
        {
            _sceneStatsData.InfectionEvents++;
            //Debug.Log("infectionEvents: "+ _sceneStatsData.InfectionEvents);
        }

        public void SaveSceneStats()
        {
            FileStream stream = new FileStream(_saveFilePath, FileMode.Create);

            _formatter.Serialize(stream, _sceneStatsData);
            stream.Close();
        }

        ///<summary>
        /// Tries to reload the savestate of the current scene.
        /// <returns> The <c>SceneStatsData</c> object if a savestate was found, null if that failed </returns>
        ///</summary>
        private SceneStatsData ReloadSceneStats()
        {
            if (File.Exists(_saveFilePath))
            {   
                try
                {
                    FileStream stream = new FileStream(_saveFilePath, FileMode.Open);
                    SceneStatsData savedStats = _formatter.Deserialize(stream) as SceneStatsData;

                    _sceneStatsData = savedStats;

                    stream.Close();
                    return _sceneStatsData;
                }
                catch(Exception e)
                {
                    Debug.LogErrorFormat("Something went wrong while loading the saved Scene data! This can indicate a damaged file at "+
                    _saveFilePath + e);
                }
            }

            return null;
        }

        ///<summary>
        /// This method takes the current stats of the scene and applies them to the overall stats of the player.
        /// After it is done it deletes the save file.
        /// Should be called once all waves in a scene are over.
        ///</summary>
        public void TransferScoreToPlayerStats()
        {   
            //TODO outsource xp calculation
            PlayerStats playerStats = new PlayerStats();

            int score = _sceneStatsData.Score;
            int xp = score / 200;

            playerStats.AddToCuredNPCS(_sceneStatsData.CuredNPCS);
            playerStats.AddToScore(score);
            playerStats.AddToXP(score / 200);

            Debug.Log("Gave "+ xp +" XP, based on a score of "+score);

            //saving the aftermath of the completed stage
            playerStats.SavePlayerStats();

            //cleaning up the no longer needed save file
            if (File.Exists(_saveFilePath))
            {
                File.Delete(_saveFilePath);
                Debug.Log("All waves over, SaveFile deleted!");
            }
        }
    }
}

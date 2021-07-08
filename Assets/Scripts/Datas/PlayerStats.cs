using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

namespace Data
{
    ///<summary>
    /// This class is used to interact with the saved progress of the player, represented by <c>PlayerStatsData</c>
    ///</summary>
    public class PlayerStats
    {
        //the name of the safe file
        public readonly string SaveFileName = "CoronaShotsPlayerStats.data";
        private string _saveFilePath;
        private BinaryFormatter _formatter = new BinaryFormatter();

        private PlayerStatsData _playerStatsData;

        public PlayerStats()
        {
            _saveFilePath = Application.persistentDataPath + "/" + SaveFileName;

            _playerStatsData = ReloadPlayerStats();

            //IF no savestate exists or could be loaded
            //THEN create a new one
            if (_playerStatsData == null)
            {
                _playerStatsData = new PlayerStatsData();
            }
        }

        public void AddToScore(int amount)
        {
            if (amount > 0)
            {
                _playerStatsData.Score += amount;
            }
        }

        public void AddToCuredNPCS(int amount)
        {
            if (amount > 0)
            {
                _playerStatsData.CuredNPCS += amount;
            }
        }

        public void AddToXP(int amount)
        {
            if (amount > 0)
            {
                _playerStatsData.XP += amount;
            }
        }

        ///<summary>
        /// Saves the player data to the file.
        ///</summary>
        public void SavePlayerStats()
        {
            FileStream stream = new FileStream(_saveFilePath, FileMode.Create);

            _formatter.Serialize(stream, _playerStatsData);
            stream.Close();
        }

        ///<summary>
        /// Tries to reload the save file of the players progress
        /// <returns> The <c>PlayerStatsData</c> object if a save was found, null if that failed </returns>
        ///</summary>
        private PlayerStatsData ReloadPlayerStats()
        {
            if (File.Exists(_saveFilePath))
            {
                try
                {
                    FileStream stream = new FileStream(_saveFilePath, FileMode.Open);
                    PlayerStatsData savedStats = _formatter.Deserialize(stream) as PlayerStatsData;

                    _playerStatsData = savedStats;

                    stream.Close();
                    return _playerStatsData;
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Something went wrong while loading the saved Scene data! This can indicate a damaged file at " +
                    _saveFilePath + e);
                }
            }

            return null;
        }

        public int GetPlayerScore()
        {
            return _playerStatsData.Score;
        }

        public int GetCuredNPCs()
        {
            return _playerStatsData.CuredNPCS;
        }

        public int GetPlayerXP()
        {
            return _playerStatsData.XP;
        }
        
        //getter and setter for the reward
        public void SetHasMaskReward(bool status)
        {
            _playerStatsData.HasMaskReward = status;
        }

        public bool GetHasMaskReward()
        {
            return _playerStatsData.HasMaskReward;
            // return true;
        }

        public void SetHasMoreAmmunitionReward(bool status)
        {
            _playerStatsData.HasMoreAmmunitionReward = status;
        }

        public bool GetHasMoreAmmuntionReward()
        {
            return _playerStatsData.HasMoreAmmunitionReward;
        }
    }
}

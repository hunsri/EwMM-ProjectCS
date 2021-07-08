using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    public class PlayerDataManager : MonoBehaviour
    {
        private PlayerData _data;
        private BinaryFormatter _formatter = new BinaryFormatter();

        // Start is called before the first frame update
        void Start()
        {
            LoadGame(false);
            if (_data == null)
            {
                _data = new PlayerData();
            }
        }

        /// <summary>
        /// Get a specific weapon by its index. Returns null if weapon with provided index
        /// is not found
        /// </summary>
        public WeaponData GetWeapon(Weapons.WeaponTags weaponIndex)
        {
            foreach (WeaponData weaponData in _data.WeaponDatas)
            {
                if (weaponData.GetWeaponIndex() == weaponIndex)
                {
                    return weaponData;
                }
            }

            return null;
        }

        /// <summary>
        /// Get list of available weapons
        /// </summary>
        public WeaponData[] GetAllWeapons()
        {
            return _data.WeaponDatas;
        }

        /// <summary>
        /// Load resource from `/Resources` folder at runtime
        /// </summary>
        public void LoadResource(string filename, Transform parent, Vector3 position)
        {
            Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
            var loadedObject = Resources.Load(filename);
            if (loadedObject == null)
            {
                throw new FileNotFoundException("...no file found - please check the configuration");
            }

            GameObject go = Instantiate(loadedObject, Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.position = parent.position;
            go.transform.position += position;
        }

        /// <summary>
        /// Save player data by serializing the `PlayerData` class and saving it to `player.data` file
        /// </summary>
        public void SaveGame()
        {
            string path = Application.persistentDataPath + "/player.data";
            FileStream stream = new FileStream(path, FileMode.Create);

            _formatter.Serialize(stream, _data);
            stream.Close();
        }

        /// <summary>
        /// Load player data by deserializing the "player.data" file
        /// </summary>
        public void LoadGame(bool reloadScene)
        {
            string path = Application.persistentDataPath + "/player.data";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                PlayerData playerData = _formatter.Deserialize(stream) as PlayerData;
                if (reloadScene)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // todo: integrate scene name to `LoadScene`
                }
                else
                {
                    _data = playerData;
                }

                stream.Close();
            }
            else
            {
                Debug.Log("No save files!");
                return;
            }
        }

        /// <summary>
        /// Save Settings
        /// </summary>
        public void SaveSettings(float vfxVolume, float musicVolume, float difficulty)
        {
            _data.UpdateSettings(vfxVolume, musicVolume, difficulty);
            SaveGame();
        }
        public bool IsDataLoaded()
        {
            return _data != null;
        }

        /// <summary>
        /// Load settings
        /// returns [_vfxVolume, _musicVolume, _difficulty]
        /// </summary>\
        public float[] LoadSettings()
        {
            if (_data == null)
            {
                LoadGame(false);
            }
            return _data.GetSettings();
        }

        /// <summary>
        /// Get normalized volume value in float (between 0 and 1)
        /// </summary>
        /// <param name="value">Value of the volume from slider that needs to be normalized</param>
        /// <returns>Normalized value of volume (between 0 and 1) that could be used with audioclips</returns>
        float GetNormalizedVolume(float value)
        {
            // slider on main menu -> max value: 0, min value: -80. Default value: -60;
            return (value + 80) / 80; // first make the value not negative by setting its max value to 80 and its min to 0
        }

        public float GetVfxVolume()
        {
            float[] settings = LoadSettings();
            return GetNormalizedVolume(settings[0]);
        }

        public float GetMusicVolume()
        {
            float[] settings = LoadSettings();
            return GetNormalizedVolume(settings[1]);
        }

        public int GetDifficulty()
        {
            float[] settings = LoadSettings();
            return (int)settings[2];
        }

        /// <summary>
        /// Add FFP Mask to the player's weapon
        /// </summary>
        public void AddFFPMask()
        {
            _data.AddWeapon(Weapons.FFPMask());
            SaveGame();
        }
    }
}
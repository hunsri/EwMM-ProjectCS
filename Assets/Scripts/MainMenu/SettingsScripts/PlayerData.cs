using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class PlayerData : MonoBehaviour
    {
        private static float _vfxVolume;
        private static float _musicVolume;
        private static float _difficulty;

        // Saves all settings in this class
        public static void SaveSettings(float vfxVolume, float musicVolume, float difficulty)
        {
            _vfxVolume = vfxVolume;
            _musicVolume = musicVolume;
            _difficulty = difficulty;
            Debug.Log("Setting are saved!");
            Debug.Log("VFX: " + _vfxVolume + ", music: " + _musicVolume + ", Difficulty: " + _difficulty + "!");
        }
    }
}

using System;

namespace Data
{
    [System.Serializable]
    public class PlayerData
    {
        WeaponData[] _weaponDatas = new WeaponData[4]; // current max num of weapons: 3
        public WeaponData[] WeaponDatas { get => _weaponDatas; set => _weaponDatas = value; }

        private float _vfxVolume;
        private float _musicVolume;
        private float _difficulty;

        public PlayerData()
        {
            _weaponDatas[0] = Weapons.MenuWeapon();
            _weaponDatas[1] = Weapons.Syringe();
            _weaponDatas[2] = Weapons.OpMask();

            // default values for all settings
            _vfxVolume = -60; // {@see: DefaultScript.cs}
            _musicVolume = -60;
            _difficulty = 0; // easy difficulty
        }

        public void AddWeapon(WeaponData weapon)
        {
            int currentLength = 3;


            if (WeaponDatas[currentLength] != null)
            {
                // weapon is full
                return;
            }

            WeaponDatas[currentLength] = weapon;
        }

        /// <summary>
        /// Setter method to set settings attributes (vfx volume, music volume and difficulty)
        /// </summary>
        public void UpdateSettings(float vfxVolume, float musicVolume, float difficulty)
        {
            _vfxVolume = vfxVolume;
            _musicVolume = musicVolume;
            _difficulty = difficulty;
        }

        /// <summary>
        /// Getter method to get all settings parameters
        /// </summary>
        public float[] GetSettings()
        {

            return new float[] { _vfxVolume, _musicVolume, _difficulty };
        }
    }
}
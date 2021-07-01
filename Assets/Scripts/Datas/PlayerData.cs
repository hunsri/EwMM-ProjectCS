using System;

[System.Serializable]
public class PlayerData
{
    WeaponData[] _weaponDatas = new WeaponData[4]; // current max num of weapons: 3
    public WeaponData[] WeaponDatas { get => _weaponDatas; set => _weaponDatas = value; }

    private static float _vfxVolume;
    private static float _musicVolume;
    private static float _difficulty;

    public PlayerData()
    {
        _weaponDatas[0] = Weapons.MenuWeapon();
        _weaponDatas[1] = Weapons.Syringe();
        _weaponDatas[2] = Weapons.OpMask();
        _weaponDatas[3] = Weapons.FFPMask();
        _vfxVolume = 1;
        _musicVolume = 1;
        _difficulty = 1;
    }

    public void AddWeapon(WeaponData weapon)
    {
        int currentLength = _weaponDatas.Length;
        if (currentLength == 3)
        {
            // weapon is full
            return;
        }

        _weaponDatas[currentLength] = weapon;
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
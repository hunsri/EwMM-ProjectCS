using System;

[System.Serializable]
public class PlayerData
{
    WeaponData[] _weaponDatas = new WeaponData[3]; // current max num of weapons: 3
    public WeaponData[] WeaponDatas { get => _weaponDatas; set => _weaponDatas = value; }

    public PlayerData()
    {
        _weaponDatas[0] = Weapons.Syringe;
        _weaponDatas[1] = Weapons.Weapon1;
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
}
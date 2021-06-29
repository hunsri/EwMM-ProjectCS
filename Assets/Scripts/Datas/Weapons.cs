using System;
public class Weapons
{
    public static WeaponData Syringe()
    {
        return new WeaponData(0, 30, new float[] { 0.12f, -0.04f, 0.01f });
    }
    // ! initiate syringe as default weapon
    // ! double check the syringe prefab at
    // ! `/Resources/Weapons/Weapon0` and `/Prefabs/Weapons/Syringe` upon changing.

    public static WeaponData Weapon1() { return new WeaponData(1, 100, new float[] { 0f, 0f, 0f }); }

    public static WeaponData Weapon2() { return new WeaponData(2, 50, new float[] { 0f, 0f, 0f }); }
}
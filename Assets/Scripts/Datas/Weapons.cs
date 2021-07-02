using System;
public class Weapons
{
    public static WeaponData Syringe()
    {
        return new WeaponData(0, 30, new float[] { 0.04f, -0.04f, 0.01f }, "Vaccine");
    }
    // ! initiate syringe as default weapon
    // ! double check the syringe prefab at
    // ! `/Resources/Weapons/Weapon0` and `/Prefabs/Weapons/Syringe` upon changing.

    public static WeaponData OpMask() { return new WeaponData(1, 1, new float[] { -0.111f, 0.012f, 0.01f }, "OP Mask"); }

    public static WeaponData FFPMask() { return new WeaponData(2, 1, new float[] { -0.111f, 0.012f, 0.01f }, "FFP2 Mask"); }
}
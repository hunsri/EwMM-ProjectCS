using System;


namespace Data
{
    public class Weapons
    {

        public enum WeaponTags
        {
            MenuWeapon = 99,
            Syringe = 0,
            OpMask = 1,
            FFPMask = 2
        }

        public static WeaponData Syringe() { return new WeaponData(WeaponTags.Syringe, 30, new float[] { 0.04f, -0.04f, 0.01f }, "Vaccine"); }
        public static WeaponData OpMask() { return new WeaponData(WeaponTags.OpMask, 50, new float[] { -0.111f, 0.012f, 0.01f }, "OP Mask"); }

        public static WeaponData FFPMask() { return new WeaponData(WeaponTags.FFPMask, 25, new float[] { -0.111f, 0.012f, 0.01f }, "FFP2 Mask"); }
        public static WeaponData MenuWeapon() { return new WeaponData(WeaponTags.MenuWeapon, 0, new float[] { 0, 0, 0 }, ""); }
    }
    // ! initiate syringe as default weapon
    // ! double check the syringe prefab at
    // ! `/Resources/Weapons/Weapon0` and `/Prefabs/Weapons/Syringe` upon changing.
}
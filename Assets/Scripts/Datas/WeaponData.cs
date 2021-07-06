using System;

namespace Data
{
    [Serializable]
    public class WeaponData
    {
        private int _weaponIndex;
        private int _ammoCount;
        private float[] _defaultPosition;
        private string _weaponName;

        /// <summary>
        /// weaponIndex: Index of the weapon
        /// maxAmmo: Max ammo of the weapon
        /// defaultPosition: Position of the weapon on render (useful to "snap" weapon to its holder)
        /// </summary>
        public WeaponData(Weapons.WeaponTags weaponIndex, int maxAmmo, float[] defaultPosition, string weaponName)
        {
            _weaponIndex = (int)weaponIndex;
            _ammoCount = maxAmmo;
            _defaultPosition = defaultPosition;
            _weaponName = weaponName;
        }

        public int GetAmmoCount()
        {
            return _ammoCount;
        }

        public void SetAmmoCount(int newAmmoCount)
        {
            _ammoCount = newAmmoCount;
        }

        public Weapons.WeaponTags GetWeaponIndex()
        {
            return (Weapons.WeaponTags)_weaponIndex;
        }

        public float[] GetDefaultPosition()
        {
            return _defaultPosition;
        }
        public string GetWeaponName()
        {
            return _weaponName;
        }
    }
}
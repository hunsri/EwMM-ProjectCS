using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Powerup
{
    public class PowerupManager : MonoBehaviour
    {

        [SerializeField]
        private int _syringeAmmo = PowerupDatas.SyringeAmmo;

        [SerializeField]
        private int _maskAmmo = PowerupDatas.MaskAmmo;

        [SerializeField]
        private int _ffpAmmo = PowerupDatas.FFPAmmo;

        private PlayerDataManager _dataManager;
        private bool _hasFFP;
        private bool _isDataLoaded = false;

        void Start()
        {
            StartCoroutine(GetPlayerData());
        }

        void Update()
        {
            if (!_isDataLoaded)
            {
                _dataManager = GetComponent<PlayerDataManager>();
                if (_dataManager)
                {
                    _isDataLoaded = _dataManager.IsDataLoaded();
                }
            }
        }

        IEnumerator GetPlayerData()
        {
            yield return new WaitUntil(() => _isDataLoaded);

            AddAmmo(Weapons.WeaponTags.Syringe, _syringeAmmo);
            AddAmmo(Weapons.WeaponTags.OpMask, _maskAmmo);

            _hasFFP = new PlayerStats().GetHasMaskReward();
            if (_hasFFP)
            {
                UnlockFFPMask();
            }
        }


        /// <summary>
        /// Will be called on starting a new wave. Add regular ammos and possibly additonal if user reaches a certain score
        /// </summary>
        public void CheckForPowerups()
        {
            bool withAdditional = new PlayerStats().GetHasMoreAmmuntionReward();
            AddAmmo(Weapons.WeaponTags.Syringe, _syringeAmmo + (withAdditional ? GetAdditional(_syringeAmmo) : 0));
            AddAmmo(Weapons.WeaponTags.OpMask, _maskAmmo + (withAdditional ? GetAdditional(_maskAmmo) : 0));

            if (_hasFFP)
            {
                AddAmmo(Weapons.WeaponTags.FFPMask, _ffpAmmo + (withAdditional ? GetAdditional(_ffpAmmo) : 0));
            }

            FindObjectOfType<WeaponHolder>().ReloadWeaponData();
        }

        /// <summary>
        /// Add ammo to the corresponding weapons
        /// </summary>
        /// <param name="weapponTag">Tag for the weapon</param>
        /// <param name="value">Value (amount) of ammo to be added</param>
        void AddAmmo(Weapons.WeaponTags weaponTag, int value)
        {
            WeaponData weapon = _dataManager.GetWeapon(weaponTag);
            if (weapon != null)
            {
                weapon.SetAmmoCount(weapon.GetAmmoCount() + value);
            }
        }

        /// <summary>
        /// Get additional ammo as a reward.
        /// </summary>
        /// <param name="value">Value of the original amount of ammo to be given.</param>
        /// <returns>The additional ammo as reward (powerup).</returns>
        int GetAdditional(int value)
        {
            return value / 2;
        }

        /// <summary>
        /// Unlocks FFP Mask for the player
        /// </summary>
        void UnlockFFPMask()
        {
            _dataManager.AddFFPMask(_ffpAmmo);
            FindObjectOfType<WeaponHolder>().ReloadWeaponData();
        }

        public bool IsLoaded()
        {
            return _isDataLoaded;
        }
    }
}
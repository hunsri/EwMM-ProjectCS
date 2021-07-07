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

        [SerializeField]
        private int _additionalSyringeAmmo = PowerupDatas.AdditionalSyringeAmmo;

        [SerializeField]
        private int _additionalMaskAmmo = PowerupDatas.AdditionalMaskAmmo;

        [SerializeField]
        private int _additionalFFPAmmo = PowerupDatas.AdditionalFFPAmmo;

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
            AddAmmo(Weapons.WeaponTags.Syringe, _syringeAmmo + (withAdditional ? _additionalSyringeAmmo : 0));
            AddAmmo(Weapons.WeaponTags.OpMask, _maskAmmo + (withAdditional ? _additionalMaskAmmo : 0));

            if (_hasFFP)
            {
                AddAmmo(Weapons.WeaponTags.OpMask, _ffpAmmo + (withAdditional ? _additionalFFPAmmo : 0));
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
        /// Unlocks FFP Mask for the player
        /// </summary>
        void UnlockFFPMask()
        {
            _dataManager.AddFFPMask();
        }

        public bool IsLoaded()
        {
            return _isDataLoaded;
        }
    }
}
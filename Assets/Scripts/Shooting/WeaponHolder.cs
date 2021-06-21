using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponHolder : MonoBehaviour
{

    [SerializeField] private int _activeWeapon = 1;

    private Canvas _canvas;
    private Text _weaponAmmo;
    private KeyCode[] _weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    // mock player's weapon data
    private int[,] _weaponData = { { 0, 30 }, { 1, 30 }, { 2, 5 } }; // array of int array (index 0: weapon index, index 1: ammo count.)

    void Start()
    {
        SelectWeapon();
        // todo: load data from PlayerData.cs script 
    }

    // Update is called once per frame
    void Update()
    {
        int currentActive = _activeWeapon;
        int weaponCount = transform.childCount - 1; // `AmmoCanvas` should be a child of `WeaponHolder`

        int index = 1;
        foreach (KeyCode key in _weaponKeys)
        {
            bool isKeyPressed = Input.GetKeyDown(key);
            if (isKeyPressed && weaponCount >= index)
            {
                _activeWeapon = index;
                break;
            }

            index++;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            if (_activeWeapon == weaponCount)
            {
                _activeWeapon = 1;
            }
            else
            {
                _activeWeapon++;
            }
        }
        else if (scroll < 0)
        {
            if (_activeWeapon == 1)
            {
                _activeWeapon = weaponCount;
            }
            else
            {
                _activeWeapon--;
            };
        }

        if (currentActive != _activeWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (weapon.TryGetComponent<Canvas>(out Canvas canvas))
            {
                // update canvas here.
                if (!_canvas)
                {
                    _canvas = canvas;
                }
            }
            else
            {
                i++;
                weapon.gameObject.SetActive(i == _activeWeapon);
                if (i == _activeWeapon)
                {
                    LoadWeaponData(weapon.GetComponent<ShootingController>());
                }
            }
        }
    }

    /// <summary>
    /// Load data assigned to a weapon by its index
    /// </summary>
    void LoadWeaponData(ShootingController shootingController)
    {
        int weaponIndex = shootingController.GetWeaponIndex();
        int index = -1;

        for (int i = 0; i <= _weaponData.Rank; i++)
        {
            if (_weaponData[i, 0] == weaponIndex)
            {
                index = i;
            }
        }

        shootingController.SetAmmo(index == -1 ? 0 : _weaponData[index, 1]);
    }

    /// <summary>
    /// Update WeaponAmmo's UI field
    /// </summary>
    public void UpdateUI(int ammoCount, int maxAmmo)
    {
        if (!_canvas)
        {
            _canvas = GetComponentInChildren<Canvas>();
        }

        if (!_weaponAmmo)
        {
            Text[] textFields = _canvas.GetComponentsInChildren<Text>();
            foreach (Text textField in textFields)
            {
                if (textField.name == "WeaponAmmo")
                {
                    _weaponAmmo = textField;
                }
            }
        }

        _weaponAmmo.text = ammoCount + " / " + maxAmmo;

        // todo mutate data on `PlayerData` class to enable saving.
    }
}

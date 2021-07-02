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
    private Text _weaponName;
    private KeyCode[] _weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    private PlayerDataManager _playerData;
    private WeaponData _activeWeaponData;
    private bool _isWeaponAccessible = false;

    void Start()
    {
        StartCoroutine(InitiatePlayerWeapon());
    }

    /// <summary>
    /// Wait for all components to be accessible
    /// </summary>
    IEnumerator InitiatePlayerWeapon()
    {
        Debug.Log("Weapon is not yet accessible");
        yield return new WaitUntil(() => _isWeaponAccessible);
        Debug.Log("Weapon is accessible!");
        InstantiateWeapons();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWeaponAccessible)
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
        else
        {
            GetPlayerData();
            if (_playerData)
            {
                _isWeaponAccessible = _playerData.IsDataLoaded();
            }
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
            else if (weapon.tag == "Hand")
            {
                return;
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

    void GetPlayerData()
    {
        _playerData = FindObjectOfType<PlayerDataManager>();
    }

    /// <summary>
    /// Load data assigned to a weapon by its index
    /// </summary>
    void LoadWeaponData(ShootingController shootingController)
    {
        int weaponIndex = shootingController.GetWeaponIndex();
        WeaponData active = _playerData.GetWeapon(weaponIndex);
        if (active == null)
        {
            Debug.Log("Weapon not found!");
            return;
        }

        _activeWeaponData = active;
        UpdateWeaponName();
        shootingController.SetAmmo(_activeWeaponData.GetAmmoCount());
    }

    /// <summary>
    /// Update UI text field based on weapon's name
    /// </summary>
    public void UpdateWeaponName()
    {
        if (!_weaponName)
        {
            GetCanvasChildren();
        }

        _weaponName.text = _activeWeaponData.GetWeaponName();
    }

    /// <summary>
    /// Get text fields from canvas.
    /// </summary>
    public void GetCanvasChildren()
    {
        Text[] textFields = _canvas.GetComponentsInChildren<Text>();
        foreach (Text textField in textFields)
        {
            if (textField.name == "WeaponAmmo")
            {
                _weaponAmmo = textField;

            }
            else if (textField.name == "WeaponName")
            {
                _weaponName = textField;
            }
        }
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
            GetCanvasChildren();
        }

        _weaponAmmo.text = ammoCount + " / " + maxAmmo;
        _activeWeaponData.SetAmmoCount(ammoCount);
    }


    /// <summary>
    /// Instantiate weapons based on the weapons in the player data.
    /// </summary>
    void InstantiateWeapons()
    {
        if (!_playerData)
        {
            GetPlayerData();
        }

        WeaponData[] weaponDatas = _playerData.GetAllWeapons();
        foreach (WeaponData weaponData in weaponDatas)
        {
            if (weaponData != null)
            {
                float[] defaultPosition = weaponData.GetDefaultPosition();
                Vector3 position = new Vector3(defaultPosition[0], defaultPosition[1], defaultPosition[2]);
                _playerData.LoadResource("Weapons/Weapon" + weaponData.GetWeaponIndex(), transform, position);
            }
        }
    }
}

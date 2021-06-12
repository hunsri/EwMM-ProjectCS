using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    [SerializeField] private int _activeWeapon = 1;
    private KeyCode[] _weaponKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int currentActive = _activeWeapon;
        int weaponCount = transform.childCount;

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
            i++;
            weapon.gameObject.SetActive(i == _activeWeapon);
        }
    }
}

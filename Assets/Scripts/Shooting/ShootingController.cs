using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class ShootingController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _projectile;
    [SerializeField] private float _projectileSpeed = 50;

    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _weaponIndex;

    private int _ammo;
    private WeaponHolder _weaponHolder;

    private Vector3 _projectileDestination;
    private Animator _animator;

    void Start()
    {
        if (!_camera)
        {
            _camera = Camera.main;
        }
        GetWeaponHolder();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isAmmoEmpty = _weaponIndex == Weapons.MenuWeaponIndex ? false : _ammo < 0;
        // get mouse left click, check if ammo is more than 0 and check if "Equip" animation is finished, check if animation is done
        if (Input.GetButtonDown("Fire1") && !isAmmoEmpty && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Equip") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {

            ShootProjectile();
        }

    }

    void ShootProjectile()
    {
        // _animator.SetBool("IsFiring", true);
        _animator.Play("Shoot");
        Ray shootingRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(shootingRay, out RaycastHit hit))
        {
            _projectileDestination = hit.point;
        }
        else
        {
            _projectileDestination = shootingRay.GetPoint(100);
        }

        InstantiateProjectile();
    }
    void InstantiateProjectile()
    {
        Vector3 projectileStartPoint = _weaponHolder.transform.position;
        var projectileObj = Instantiate(_projectile, projectileStartPoint, Quaternion.Euler(transform.forward));

        projectileObj.transform.rotation = transform.rotation; // set projectile's rotation to face user

        projectileObj.GetComponent<Rigidbody>().velocity = (_projectileDestination - projectileStartPoint).normalized * _projectileSpeed;
        if (_weaponIndex != Weapons.MenuWeaponIndex)
        {
            SetAmmo(_ammo - 1);
        }
    }

    public void SetAmmo(int ammoCount)
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }

        if (ammoCount <= 0)
        {
            _animator.SetBool("IsEmpty", true);
        }
        else
        {
            if (_animator.GetBool("IsEmpty"))
            {
                _animator.SetBool("IsEmpty", false);
            }
        }


        _ammo = ammoCount;

        // It is possible that weapon holder is not yet defined as parent when the `Start()` method is called.
        if (!_weaponHolder)
        {
            GetWeaponHolder();
        }
        _weaponHolder.UpdateUI(ammoCount, _maxAmmo);
    }

    public int GetWeaponIndex()
    {
        return _weaponIndex;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }

    void GetWeaponHolder()
    {
        _weaponHolder = GetComponentInParent<WeaponHolder>();
        gameObject.transform.TransformPoint(Vector3.zero);
    }
}

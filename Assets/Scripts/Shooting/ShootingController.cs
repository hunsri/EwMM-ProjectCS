using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        if (!_camera)
        {
            _camera = Camera.main;
        }

        _ammo = _maxAmmo;
        GetWeaponHolder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _ammo > 0)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
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
        Vector3 projectileStartPoint = transform.position;
        var projectileObj = Instantiate(_projectile, projectileStartPoint, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (_projectileDestination - projectileStartPoint).normalized * _projectileSpeed;
        SetAmmo(_ammo - 1);
    }

    public void SetAmmo(int ammoCount)
    {
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

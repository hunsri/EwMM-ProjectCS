using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _projectile;
    [SerializeField] private float _projectileSpeed = 50;

    private Vector3 _projectileDestination;

    void Start()
    {
        if (!_camera)
        {
            _camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private Transform projectile;
    [SerializeField] private float projectileSpeed = 3;

    private Vector3 projectileDestination;

    void Start()
    {
        if (!camera)
        {
            camera = Camera.main;
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
        Ray shootingRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(shootingRay, out RaycastHit hit))
        {
            projectileDestination = hit.point;
        }
        else
        {
            projectileDestination = shootingRay.GetPoint(100);
        }

        InstantiateProjectile();
    }

    void InstantiateProjectile()
    {
        Vector3 projectileStartPoint = transform.position;
        var projectileObj = Instantiate(projectile, projectileStartPoint, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (projectileDestination - projectileStartPoint).normalized * projectileSpeed;
    }
}

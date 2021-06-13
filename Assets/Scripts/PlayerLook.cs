using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _cameraTransform;

    // keep changes of rotation around the x-axis (looking up + down)
    private float _xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // hide cursor.
    }

    void Update()
    {
        float mouseX = getNormalized(Input.GetAxis("Mouse X")); // unity premapped
        float mouseY = getNormalized(Input.GetAxis("Mouse Y")); // unity premapped

        // rotate around x axis for looking up and down
        _xRotation -= mouseY; // decreasing instead of increasing => rotation is flipped otherwise
        _xRotation = Mathf.Clamp(_xRotation, -90, 90); // prevent (up+dpwn) rotation for more than +- 90 deg

        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    float getNormalized(float value)
    {
        return value * _mouseSensitivity * Time.deltaTime;
    }
}

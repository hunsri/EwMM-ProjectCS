using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

 public float mouseSensitivity = 100f;
 public Transform cameraTransform;

 // keep changes of rotation around the x-axis (looking up + down)
 private float xRotation;

 void Start()
 {
  Cursor.lockState = CursorLockMode.Locked; // hide cursor.
 }

 // Update is called once per frame
 void Update()
 {
  // * tuts: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=UnityUnityVerified
  float mouseX = getNormalized(Input.GetAxis("Mouse X")); // unity premapped
  float mouseY = getNormalized(Input.GetAxis("Mouse Y")); // unity premapped

  // rotate around x axis for looking up and down
  xRotation -= mouseY; // decreasing instead of increasing => rotation is flipped otherwise
  xRotation = Mathf.Clamp(xRotation, -90, 90); // prevent (up+dpwn) rotation for more than +- 90 deg

  cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
  transform.Rotate(Vector3.up * mouseX);
 }

 float getNormalized(float value)
 {
  return value * mouseSensitivity * Time.deltaTime;
 }
}

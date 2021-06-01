using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
 public float speed = 2;

 Vector3 velocity;

 // Start is called before the first frame update
 void Start()
 {

 }

 // Update is called once per frame
 void Update()
 {
  float moveX = Input.GetAxis("Horizontal");
  float moveY = Input.GetAxis("Vertical");

  Vector3 move = transform.right * moveX + transform.forward * moveY;
  transform.position += move * Time.deltaTime * speed;
 }
}

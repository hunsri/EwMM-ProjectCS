using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UninfectedBehavior : MonoBehaviour
{
    private int _moveSpeed = 5;
    private float _rotSpeed = 1;

    private float _angle;
    private Vector3 _targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        _angle = randomAngle();
        _targetDirection = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle));
    }

    // Update is called once per frame
    void Update()
    {
        //calculating new viewing angle and targeted direction
        transform.rotation = Quaternion.LookRotation(_targetDirection);

        transform.position += transform.rotation * new Vector3(0, 0, 1) * _moveSpeed * Time.deltaTime;
    }

     private void OnCollisionEnter(Collision other){
        GameObject go = other.gameObject;
        
        Debug.Log(go.name);

        if(go.name == "NPCBorder"){
            _angle += 110;
            _targetDirection = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle));
        }

     }

    private float randomAngle(){
        float randomValue = UnityEngine.Random.Range(0f, 360f);

        return randomValue;
    }
}

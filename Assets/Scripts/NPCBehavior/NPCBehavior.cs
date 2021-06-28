using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Behavior;

namespace Behavior
{
    public class NPCBehavior : MonoBehaviour
    {
        [SerializeField]
        private Material _uninfectedMaterial;
        [SerializeField]
        private Material _infectedMaterial;
        [SerializeField]
        private Material _curedMaterial;

        private GameObject _indicator;

        private Behaviors _behavior;

        private float _angle;
        private Vector3 _targetDirection;

        // Start is called before the first frame update
        void Start()
        {
            _behavior = Behaviors.UNINFECTED;

            _indicator = findNPCIndicator();
            _indicator.GetComponent<Renderer>().material = _uninfectedMaterial;

            _angle = randomAngle();
            _targetDirection = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle));
            transform.rotation = Quaternion.LookRotation(_targetDirection);
        }

        // Update is called once per frame
        void Update()
        {

            //going forward in the facing direction
            //really dislike the implementation here, maybe splitting it into individual scripts later
            float speed;

            switch(_behavior)
            {   
                case Behaviors.UNINFECTED:
                    speed = 4;
                    break;
                case Behaviors.INFECTED:
                    speed = 2;
                    break;
                case Behaviors.CURED:
                    speed = 4;
                    break;
                default:
                    speed = 0;
                    break;
            }

            transform.position += transform.rotation * new Vector3(0, 0, 1) * speed  * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject go = other.gameObject;

            //if the collided object has the Border tag change direction 
            if(go.tag == "Border")
            {
                transform.rotation *= Quaternion.AngleAxis( 180, transform.up ); 
                changeBehavior(Behaviors.INFECTED);
            }

            if(go.tag == "Projectile")
            {
                changeBehavior(Behaviors.CURED);
            }

        }

        private float randomAngle()
        {
            float randomValue = UnityEngine.Random.Range(0f, 360f);

            return randomValue;
        }

        private void changeBehavior(Behaviors b)
        {
            _behavior = b;

            //changing the material of the indicator 
            switch(_behavior)
            {
                case Behaviors.UNINFECTED:
                _indicator.GetComponent<Renderer>().material = _uninfectedMaterial;
                break;
                case Behaviors.INFECTED:
                _indicator.GetComponent<Renderer>().material = _infectedMaterial;
                break;
                case Behaviors.CURED:
                _indicator.GetComponent<Renderer>().material = _curedMaterial;
                break;
            }
        }

        private GameObject findNPCIndicator()
        {
            Component[] components = gameObject.GetComponentsInChildren<Transform>();

            for(int i = 0; i < components.Length; i++)
            {
                if (components[i].name == "MarkerSphere"){
                    return components[i].gameObject;
                }
            }

            return null;
        }
    }
}

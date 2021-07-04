using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
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

        //default behavior is uninfected
        //the behavior gets set later during spawn (e.g. from NPCSpawner)
        private Behaviors _behavior = Behaviors.UNINFECTED;

        private Vector3 _targetDirection;

        //the default probability of infecting someone else in percent
        private const int SpreadingPercent = 10;

        void Awake()
        {
            _indicator = FindNPCIndicator();
            _indicator.GetComponent<Renderer>().material = _uninfectedMaterial;
            _indicator.GetComponent<Renderer>().sortingOrder = 99;
        }

        // Start is called before the first frame update
        void Start()
        {
            // _indicator = findNPCIndicator();
            // _indicator.GetComponent<Renderer>().material = _uninfectedMaterial;

            //spawning with a random LookRotation
            float angle = RandomAngle(360f);
            _targetDirection = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
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
            }

            if(go.tag == "KillBorder")
            {
                Destroy(this.gameObject);
            }

            //TODO change accordingly to projectile type
            if(go.tag == "Projectile")
            {
                ChangeBehavior(Behaviors.CURED);
            }

            if(go.tag == "Enemy")
            {
                NPCBehavior script;

                if(_behavior == Behaviors.INFECTED){

                    int probability = UnityEngine.Random.Range(1, 100);
                    
                    if(probability < SpreadingPercent)
                    {
                        script = go.GetComponent<NPCBehavior>();

                        script.ChangeBehavior(Behaviors.INFECTED);
                    }
                }

                //when colliding with another NPC the direction gets slightly changed
                transform.rotation *= Quaternion.AngleAxis( RandomAngle(45f), transform.up );

            }

        }
        private float RandomAngle(float max)
        {
            float randomValue = UnityEngine.Random.Range(0f, max);

            return randomValue;
        }

        public void ChangeBehavior(Behaviors b)
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

        private GameObject FindNPCIndicator()
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

        public Behaviors GetBehaviors(){
            return _behavior;
        }
    }
}

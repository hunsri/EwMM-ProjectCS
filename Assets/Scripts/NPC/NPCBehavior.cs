using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Data;

namespace NPC
{
    ///<summary>
    /// This script is intended to be applied to a NPC.
    /// This script requires that the SceneControllerObject is present in the scene which has <c>NPCWaveManager</c> attached to it
    ///</summary>
    public class NPCBehavior : MonoBehaviour
    {
        [SerializeField]
        private Material _uninfectedMaterial;
        [SerializeField]
        private Material _infectedMaterial;
        [SerializeField]
        private Material _curedMaterial;

        [SerializeField]
        private bool _isNoVac = false;

        [SerializeField]
        private bool _isNoMask = false;

        [SerializeField]
        private GameObject _noVacBoard;

        [SerializeField]
        private GameObject _noMaskBoard;

        [SerializeField]
        private GameObject _maskObject;

        [SerializeField]
        private GameObject _ffp2MaskObject;
        private GameObject _indicator;

        //default behavior is uninfected
        //the behavior gets set later during spawn (e.g. from NPCSpawner)
        private Behaviors _behavior = Behaviors.UNINFECTED;

        private Vector3 _targetDirection;

        //the default probability of infecting someone else in percent
        private const int SpreadingPercent = 10;

        //time interval between infected sounds
        private float _timeInterval;

        //holds information about the NPC waves; in this case interesting for checking if the waves are paused
        private NPCWaveManager _waveManager;
        private Animator _animator;

        private MaskType _isMasked = MaskType.NONE; // between 0 and 2, 0 = nomask, 1 = op mask, 2 = ffp mask
        public ParticleSystem infectedDrops;

        void Awake()
        {
            _indicator = FindNPCIndicator();
            Renderer renderer = _indicator.GetComponent<Renderer>();
            renderer.material = _uninfectedMaterial;
            renderer.sortingOrder = 99;


        }

        // Start is called before the first frame update
        void Start()
        {
            //spawning with a random LookRotation
            float angle = RandomAngle(360f);
            _targetDirection = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            transform.rotation = Quaternion.LookRotation(_targetDirection);

            UpdateAppearance();

            SetTimeInterval();

            _waveManager = FindObjectOfType<NPCWaveManager>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //going forward in the facing direction
            //really dislike the implementation here, maybe splitting it into individual scripts later
            float speed;

            switch (_behavior)
            {
                case Behaviors.UNINFECTED:
                    speed = 4;
                    break;
                case Behaviors.INFECTED:
                    speed = 2;
                    triggerInfectedSoundsInInterval();
                    break;
                case Behaviors.CURED:
                    speed = 4;
                    break;
                default:
                    speed = 0;
                    break;
            }

            if (!_waveManager.IsPaused)
            {
                _animator.enabled = true;
                transform.position += transform.rotation * new Vector3(0, 0, 1) * speed * Time.deltaTime;
            }
            else
            {
                _animator.enabled = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            //only process collisions if the game isn't paused
            if (!_waveManager.IsPaused)
            {
                GameObject go = other.gameObject;

                //if the collided object has the Border tag change direction 
                if (go.tag == "Border")
                {
                    transform.rotation *= Quaternion.AngleAxis(180, transform.up);
                }

                //delete the NPC if it touches the limits of the world
                if (go.tag == "KillBorder")
                {
                    Destroy(this.gameObject);
                }

                if (go.tag == "Projectile")
                {
                    Weapons.WeaponTags weaponTag = go.GetComponent<ProjectileController>().GetTag();
                    if (!_isNoVac)
                    {
                        if (weaponTag == Weapons.WeaponTags.Syringe)
                        {
                            ChangeBehavior(Behaviors.CURED);
                        }
                    }

                    if (!_isNoMask)
                    {
                        if (weaponTag == Weapons.WeaponTags.OpMask)
                        {
                            if (_isMasked != MaskType.NONE)
                            {
                                return;
                            }
                            _isMasked = MaskType.NORMAL;
                        }

                        if (weaponTag == Weapons.WeaponTags.FFPMask)
                        {
                            if (_isMasked == MaskType.FFP)
                            {
                                return;
                            }
                            _isMasked = MaskType.FFP;
                        }

                        UpdateAppearance();
                    }
                }

                //calculating if an NPC infects another NPC if it touches it
                //also slightly changing the direction in case an NPC touches another NPC
                if (go.tag == "Enemy")
                {
                    NPCBehavior script;

                    if (_behavior == Behaviors.INFECTED)
                    {

                        int probability = UnityEngine.Random.Range(1, 100);

                        if (probability < SpreadingPercent)
                        {
                            script = go.GetComponent<NPCBehavior>();

                            script.ChangeBehavior(Behaviors.INFECTED);
                        }
                    }

                    //when colliding with another NPC the direction gets slightly changed
                    transform.rotation *= Quaternion.AngleAxis(RandomAngle(45f), transform.up);

                }
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
            switch (_behavior)
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

        /// <summary>
        /// Update appearance of the npc
        /// </summary>
        void UpdateAppearance()
        {
            GameObject[] appearanceObjects = new GameObject[] { _noMaskBoard, _noVacBoard, _maskObject, _ffp2MaskObject };
            // check if all appearance objects exist
            if (Array.TrueForAll(appearanceObjects, (obj) => obj != null))
            {
                _maskObject.SetActive(_isMasked == MaskType.NORMAL);
                _ffp2MaskObject.SetActive(_isMasked == MaskType.FFP);
                _noMaskBoard.SetActive(_isNoMask);
                _noVacBoard.SetActive(_isNoVac);
            }
        }

        ///<summary>
        /// Searching for the GameObject that is attached to the NPC that marks and displays its state for the player 
        ///</summary>
        private GameObject FindNPCIndicator()
        {
            Component[] components = gameObject.GetComponentsInChildren<Transform>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].name == "MarkerSphere")
                {
                    return components[i].gameObject;
                }
            }

            return null;
        }

        public Behaviors GetBehaviors()
        {
            return _behavior;
        }

        public void PlaySound()
        {
            int AudioClipRandom = UnityEngine.Random.Range(1, 6);
            Debug.Log("Sound" + AudioClipRandom);
            SoundManager.soundManager.PlayRandomInfectedSounds(AudioClipRandom, transform.position);
        }

        public void SetTimeInterval()
        {
            _timeInterval = UnityEngine.Random.Range(5.0f, 60.0f);
            Debug.Log("Set:" + _timeInterval);
        }

        public void triggerInfectedSoundsInInterval()
        {
            _timeInterval -= Time.deltaTime;

            if (_timeInterval <= 0)
            {

                PlaySound();
                infectedDrops.Play();
                SetTimeInterval();
            }
        }

        /// <summary>
        /// For debug purposes.
        /// </summary>
        public void SetBehaviors(bool isNoMask, bool isNoVac, MaskType maskType)
        {
            _isMasked = maskType;
            _isNoMask = isNoMask;
            _isNoVac = isNoVac;
            UpdateAppearance();
        }
    }
}

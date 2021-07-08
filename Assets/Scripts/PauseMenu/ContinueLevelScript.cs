using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NPC;

namespace PauseMenu
{
    public class ContinueLevelScript : MonoBehaviour
    {
        // VR Variables
        [SerializeField]
        private InputActionReference _pressReference = null;

        [SerializeField]
        private GameObject _tent;

        [SerializeField]
        private GameObject _ui;

        private Animator _animator;

        private bool _isTriggered = false;
        private bool _isDown = false;

        private NPCWaveManager _np;

        // Start is called before the first frame update
        void Start()
        {
            _np = FindObjectOfType<NPCWaveManager>();
            _animator = _tent.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isTriggered)
            {
                if (!_isDown)
                {
                    _isTriggered = false;
                    _animator.ResetTrigger("shouldDespawn");
                    _animator.SetTrigger("shouldSpawn");
                    _isDown = true;
                    _np.IsPaused = true;
                }
                else if (_isDown)
                {
                    _isTriggered = false;
                    _animator.ResetTrigger("shouldSpawn");
                    _animator.SetTrigger("shouldDespawn");
                    _isDown = false;
                }
                else
                {
                    Debug.Log("Something went bad on Update() in ContinueLevelScript!");
                    _isTriggered = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                if (!_isDown)
                {
                    _animator.ResetTrigger("shouldDespawn");
                    _animator.SetTrigger("shouldSpawn");
                    _isDown = true;
                    _np.IsPaused = true;
                }
                else if (_isDown)
                {
                    _animator.ResetTrigger("shouldSpawn");
                    _animator.SetTrigger("shouldDespawn");
                    _isDown = false;
                }
                else
                {
                    Debug.Log("Something went bad on Update() in ContinueLevelScript!");
                }
            }
        }

        // Will be used when the current object is triggered
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                Debug.Log("Hit Continue");
                _isTriggered = true;
            }
        }

        public void DisableUI(){
            _ui.SetActive(false);
        }

        public void EnableUI(){
            _ui.SetActive(true);
        }

        public void UnPause(){
            _np.IsPaused = false;
        }

        // VR funtions
        private void Awake()
        {
            _pressReference.action.started += OnPressPause;
        }

        private void OnDestroy()
        {
            _pressReference.action.started += OnPressPause;
        }

        private void OnPressPause(InputAction.CallbackContext context)
        {
            _isTriggered = true;
        }
    }
}
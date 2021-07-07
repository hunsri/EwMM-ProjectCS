using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC;

namespace PauseMenu
{
    public class ContinueLevelScript : MonoBehaviour
    {
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
            Debug.Log("Hit Continue");
            _isTriggered = true;
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
    }
}
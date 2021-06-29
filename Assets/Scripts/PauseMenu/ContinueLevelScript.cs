using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PauseMenu
{
    public class ContinueLevelScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tent;

        private Animator _animator;

        private bool _isTriggered = false;
        private bool _isDown = false;

        // Start is called before the first frame update
        void Start()
        {
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
        }

        // Will be used when the current object is triggered
        private void OnTriggerEnter(Collider other)
        {
            _isTriggered = true;
        }
    }
}
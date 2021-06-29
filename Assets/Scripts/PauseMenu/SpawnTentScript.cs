using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PauseMenu
{
    public class SpawnTentScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tent;

        private Animator _animator;

        private bool _isDown = false;

        // Start is called before the first frame update
        void Start()
        {
            _animator = _tent.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                if (!_isDown)
                {
                    _animator.ResetTrigger("shouldDespawn");
                    _animator.SetTrigger("shouldSpawn");
                    _isDown = true;
                }
                else if (_isDown)
                {
                    _animator.ResetTrigger("shouldSpawn");
                    _animator.SetTrigger("shouldDespawn");
                    _isDown = false;
                }
                else
                {
                    Debug.Log("Something went bad on Update() in SpawnTentScript!");
                }
            }
        }
    }
}
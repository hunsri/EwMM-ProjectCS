using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC;


namespace PauseMenu
{
    public class SpawnTentScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tent;

        [SerializeField]
        private GameObject _ui;

        private Animator _animator;

        [SerializeField]
        private GameObject _contoller;

        private bool _isDown = false; // equivalent to isPaused

        private NPCWaveManager _np;

        // Start is called before the first frame update
        void Start()
        {
            _np = _contoller.GetComponent<NPCWaveManager>();
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
                    _np.IsPaused = true;

                }
                else if (_isDown)
                {
                    _animator.ResetTrigger("shouldSpawn");
                    _animator.SetTrigger("shouldDespawn");
                    _isDown = false;
                    _np.IsPaused = false;
                }
                else
                {
                    Debug.Log("Something went bad on Update() in SpawnTentScript!");
                }
            }
        }

        public bool getIsDown(){
            return _isDown;
        }

        public void DisableUI(){
            _ui.SetActive(false);
        }

        public void EnableUI(){
            _ui.SetActive(true);
        }

        public void pauseTime(){
           // _np.IsPaused = true;
        }
    }
}
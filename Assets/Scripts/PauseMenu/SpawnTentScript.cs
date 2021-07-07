using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PauseMenu
{
    public class SpawnTentScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tent;

        [SerializeField]
        private GameObject _ui;

        private Animator _animator;

        private bool _isDown = false; // equivalent to isPaused

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
                    //DisableUI();
                }
                else if (_isDown)
                {
                    _animator.ResetTrigger("shouldSpawn");
                    _animator.SetTrigger("shouldDespawn");
                    _isDown = false;
                    //Time.timeScale = 1;
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
            Time.timeScale = 0; 
        }
    }
}
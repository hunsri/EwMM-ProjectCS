using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLevelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _tent;

    //private Animator _animator;

    private bool _isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //_animator = _tent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTriggered)
        {
            if (!_tent.activeSelf)
            {
                _tent.SetActive(true);
                _isTriggered = false;
                //_animator.SetTrigger("shouldSpawn");
            }
            else if (_tent.activeSelf)
            {
                //_animator.SetTrigger("shouldDespawn");
                _tent.SetActive(false);
                _isTriggered = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTentScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _tent;

    //private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _tent.SetActive(false);
        //_animator = _tent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_tent.activeSelf)
            {
                _tent.SetActive(true);
                //_animator.SetTrigger("shouldSpawn");
            }
            else if (_tent.activeSelf)
            {
                //_animator.SetTrigger("shouldDespawn");
                _tent.SetActive(false);
            }
            else
            {
                Debug.Log("Something went bad on Update() in SpawnTentScript!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (!_tent.activeSelf)
            {
                _tent.SetActive(true);
                //_animator.SetTrigger("shouldSpawn");
            }
            else if (_tent.activeSelf)
            {
                //_animator.SetTrigger("shouldDespawn");
                _tent.SetActive(false);
            }
            else
            {
                Debug.Log("Something went bad on Update() in SpawnTentScript!");
            }
        }
    }
}

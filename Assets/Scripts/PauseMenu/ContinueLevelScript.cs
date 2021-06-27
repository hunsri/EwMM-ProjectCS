using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLevelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _tent;

    private bool _isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {

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
            }
            else if (_tent.activeSelf)
            {
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

    private void OnTriggerEnter(Collider other)
    {
        _isTriggered = true;
    }
}

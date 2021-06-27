using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _tent;

    // Start is called before the first frame update
    void Start()
    {
        _tent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            if (_tent.activeSelf == false)
            {
                _tent.SetActive(true);
            }
            else if (_tent.activeSelf == true)
            {
                _tent.SetActive(false);
            }
        }
    }
}

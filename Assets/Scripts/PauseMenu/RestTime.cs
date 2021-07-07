using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestTime : MonoBehaviour
{
    [SerializeField] private Text _restTime;

    [SerializeField] private Text _time;
    // Start is called before the first frame update
    void Update()
    {
        _restTime.text = _time.text;
    }
}
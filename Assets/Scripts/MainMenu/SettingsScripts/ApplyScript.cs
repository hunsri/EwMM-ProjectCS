using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyScript : MonoBehaviour
{
    [SerializeField]
    private Slider _vfxSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private GameObject _easyPlane;
    [SerializeField]
    private GameObject _mediumPlane;
    [SerializeField]
    private GameObject _hardPlane;

    private MeshRenderer _easyRenderer;
    private MeshRenderer _mediumRenderer;
    private MeshRenderer _hardRenderer;

    private string _shaderPropertyName = "_Color";

    // Start is called before the first frame update
    void Start()
    {
        _easyRenderer = _easyPlane.GetComponent<MeshRenderer>();
        _mediumRenderer = _mediumPlane.GetComponent<MeshRenderer>();
        _hardRenderer = _hardPlane.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Not needed jet
    }

    // Will be called if the current object is triggered
    private void OnTriggerEnter(Collider other)
    {
        float vfxVolume = _vfxSlider.value;
        float musicVolume = _musicSlider.value;
        float difficulty = -1;

        if (_easyRenderer.material.GetColor(_shaderPropertyName) == Color.black)
        {
            difficulty++;
            if (_mediumRenderer.material.GetColor(_shaderPropertyName) == Color.black)
            {
                difficulty++;
                if (_hardRenderer.material.GetColor(_shaderPropertyName) == Color.black)
                {
                    difficulty++;
                }
            }
        }
        else if (difficulty == -1)
        {
            Debug.Log("Something went bad on OnTriggerEnter() in ApplyScript!");
        }

        PlayerData.saveSettings(vfxVolume, musicVolume, difficulty);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SliderSettingScript : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private AudioMixer _audioMixer;

    private MeshRenderer _renderer;
    private bool _isUpdated = false;
    private float _volumeNumber;
    private string _shaderPropertyName = "_Color";
    private Color color = Color.white;

    

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _audioMixer.GetFloat("volume", out _volumeNumber);
        _slider.value = _volumeNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUpdated)
        {
            SetVolume(_volumeNumber);
            _renderer.material.SetColor(_shaderPropertyName, color);
            _isUpdated = false;
        }
    }

    public void SetVolume(float volume)
    {
        _slider.value = volume;
        _audioMixer.SetFloat("volume", volume);
    }

    public void TargetReact()
    {
        _audioMixer.GetFloat("volume", out _volumeNumber);
        Debug.Log(name + " Game Object Clicked!");
        _renderer.material.SetColor(_shaderPropertyName, Color.black);
        if (name == "Plus Button")
        {
            if (_volumeNumber !< 0)
            {
                _volumeNumber = _volumeNumber + 10;
                _isUpdated = true;
            } else
            {
                _renderer.material.SetColor(_shaderPropertyName, color);
            }
        }
        else if (name == "Minus Button")
        {
            if (_volumeNumber != -80)
            {
                _volumeNumber = _volumeNumber - 10;
                _isUpdated = true;
            } else
            {
                _renderer.material.SetColor(_shaderPropertyName, color);
            }
        }
    }
}
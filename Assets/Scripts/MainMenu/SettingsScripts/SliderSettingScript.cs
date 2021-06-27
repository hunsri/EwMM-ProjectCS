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

    private bool _musicIsUpdated = false;
    private float _musicVolumeNumber;

    private bool _sfxIsUpdated = false;
    private float _sfxVolumeNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (_slider.name == "Music Slider")
        {
            _audioMixer.GetFloat("musicVolume", out _musicVolumeNumber);
            _slider.value = _musicVolumeNumber;
        } 
        else if (_slider.name == "SFX Slider")
        {
            _audioMixer.GetFloat("sfxVolume", out _sfxVolumeNumber);
            _slider.value = _sfxVolumeNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_musicIsUpdated)
        {
            SetMusicVolume(_musicVolumeNumber);
            _musicIsUpdated = false;
        }
        if (_sfxIsUpdated)
        {
            SetSFXVolume(_sfxVolumeNumber);
            _sfxIsUpdated = false;
        }
    }

    // Will set the current volume to the slider and the audio mixer
    // The audio mixer will ajust the music volume of the game
    public void SetMusicVolume(float volume)
    {
        _slider.value = volume;
        _audioMixer.SetFloat("musicVolume", volume);
    }

    // Will set the current volume to the slider and the audio mixer
    // The audio mixer will ajust the SFX volume of the game
    public void SetSFXVolume(float volume)
    {
        _slider.value = volume;
        _audioMixer.SetFloat("sfxVolume", volume);
    }

    // Will be called if the user hits one of the arrows
    public void TargetReact()
    {
        if (_slider.name == "Music Slider")
        {
            _audioMixer.GetFloat("musicVolume", out _musicVolumeNumber);

            if (name == "Right Arrow Music Button")
            {
                if (_musicVolumeNumber < -1)
                {
                    _musicVolumeNumber = _musicVolumeNumber + 10;
                    _musicIsUpdated = true;
                }
            }
            else if (name == "Left Arrow Music Button")
            {
                if (_musicVolumeNumber > -80)
                {
                    _musicVolumeNumber = _musicVolumeNumber - 10;
                    _musicIsUpdated = true;
                }
            }
        } else if (_slider.name == "SFX Slider")
        {
            _audioMixer.GetFloat("sfxVolume", out _sfxVolumeNumber);

            if (name == "Right Arrow SFX Button")
            {
                if (_sfxVolumeNumber < -1)
                {
                    _sfxVolumeNumber = _sfxVolumeNumber + 10;
                    _sfxIsUpdated = true;
                }
            }
            else if (name == "Left Arrow SFX Button")
            {
                if (_sfxVolumeNumber > -80)
                {
                    _sfxVolumeNumber = _sfxVolumeNumber - 10;
                    _sfxIsUpdated = true;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Data;


public class SoundManager : MonoBehaviour
{

    public static SoundManager soundManager;

    private AudioSource _audioSource;
    private AudioSource _weaponAudioSource;
    // public GameObject infectedPrefab;

    [SerializeField] private AudioClip _coughing, _sneezing;
    [SerializeField] private AudioClip _background;

    // [SerializeField]
    private float _vfxVolume = -60; // default value, if there's something wrong while loading the data
    private bool _isSettingsAccessible = false;
    private PlayerDataManager _dataManager;
    [SerializeField] private AudioClip  _throw, _useVaccine, _switchAmmo;
    [SerializeField] private AudioClip _levelComplete, _gameOver, _waveIncoming;

    private void Awake()
    {
        soundManager = this;
        DontDestroyOnLoad(gameObject);

        AudioSource[] audioSources = GetComponents<AudioSource>();
        //quick fix
        //program crashes if array is not filled!
        if(audioSources.Length > 1)
        {
            _audioSource = audioSources[0];
            _weaponAudioSource = audioSources[1];
        }
    }

    void Start()
    {
        StartCoroutine(GetUserVolume());
    }

    /// <summary>
    /// Load vfx volume from saved user setting
    /// </summary>
    IEnumerator GetUserVolume()
    {
        Debug.Log("Loading user settings");
        yield return new WaitUntil(() => _isSettingsAccessible);
        Debug.Log("Loading is successful");
        _vfxVolume = _dataManager.GetVfxVolume();
    }

    /// <summary>
    /// Try to find loaded user data
    /// </summary>
    void Update()
    {
        if (!_isSettingsAccessible)
        {
            _dataManager = FindObjectOfType<PlayerDataManager>();
            if (_dataManager != null)
            {
                _isSettingsAccessible = _dataManager.IsDataLoaded();
            }
        }
    }

    public void PlayRandomInfectedSounds(int sound, Vector3 position)
    {
        switch (sound)
        {
            case 1:
            case 3:
            case 5:
                Cough(position);
                break;
            case 2:
            case 4:
            case 6:
                Sneeze(position);
                break;
        }
    }

    public void PlayProjectileUsed(int index, Vector3 position)
    {
       // if (index == 0)
       // {
       //     AudioSource.PlayClipAtPoint(_useVaccine, position);

        //quick fix 
        if(_weaponAudioSource != null)
        {
            _weaponAudioSource.loop = false;
            if (index == 0)
            {
                _weaponAudioSource.clip = _useVaccine;
            }
            else if (index == 1 || index == 2)
            {
                //AudioSource.PlayClipAtPoint(_throw, position);
                // }
                _weaponAudioSource.clip = _throw;
            }
            _weaponAudioSource.volume = _vfxVolume * .2f;
            _weaponAudioSource.Play();
        }
    }

    public void PlaySwitchAmmo(Vector3 position)
    {
        if(_switchAmmo != null)
            AudioSource.PlayClipAtPoint(_switchAmmo, position, _vfxVolume);
    }

    public void Cough(Vector3 position)
    {
        if(_coughing != null)
            AudioSource.PlayClipAtPoint(_coughing, position, _vfxVolume);
    }

    void Sneeze(Vector3 position)
    {
        if(_sneezing != null)
            AudioSource.PlayClipAtPoint(_sneezing, position, _vfxVolume);
    }

    public IEnumerator BgSound()
    {
        yield return new WaitUntil(() => _isSettingsAccessible);
        _audioSource.loop = true;
        _audioSource.clip = _background;
        _audioSource.volume = _dataManager.GetMusicVolume();
        _audioSource.Play();
    }

    public void LevelSounds(int type){
        switch(type){
            case 0:
                if(_levelComplete != null)
                    _audioSource.clip = _levelComplete;
                break;
            case 1:
                if(_gameOver != null)
                    _audioSource.clip = _gameOver;
                break;
            case 2:
                if(_waveIncoming != null)
                    _audioSource.clip = _waveIncoming;
                break;
        }
        if(_audioSource != null)
        {
            _audioSource.volume = 0.2f;
            _audioSource.Play();
        }
    }
}

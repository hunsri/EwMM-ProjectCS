using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SoundManager : MonoBehaviour
{

    public static SoundManager soundManager;

    private AudioSource _audioSource;

   // public GameObject infectedPrefab;

    [SerializeField] private AudioClip _coughing, _sneezing;
    [SerializeField] private AudioClip _background;
    [SerializeField] private AudioClip  _throw, _useVaccine, _switchAmmo;
    [SerializeField] private AudioClip _levelComplete, _gameOver, _waveIncoming;
   

    private void Awake()
    {
     
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        

        _audioSource = GetComponent<AudioSource>();
       
    }

    public void PlayRandomInfectedSounds(int sound, Vector3 position)
    {
        switch (sound)
        {
            case 1:
            case 3:
            case 5: Cough(position);
                break;
            case 2:
            case 4:
            case 6: Sneeze(position);
                break;
        }
    }

    public void PlayProjectileUsed(int index, Vector3 position)
    {
        _audioSource.loop = false;
        if (index == 0)
        {
            _audioSource.clip = _useVaccine;
        }
        else if(index == 1 || index == 2)
        {
            _audioSource.clip = _throw;
        }
        _audioSource.volume = 0.1f;
        _audioSource.Play();
    }

    public void PlaySwitchAmmo(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_switchAmmo, position);
    }

    public void Cough(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_coughing, position);    
    }

    void Sneeze(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_sneezing, position);  
    }

   public  void BgSound()
    {
        _audioSource.loop = true;
        _audioSource.clip = _background;
        _audioSource.volume = 0.2f;
        _audioSource.Play();   
    }

    public void LevelSounds(int type){
        switch(type){
            case 0: 
                _audioSource.clip = _levelComplete;
                break;
            case 1:
                _audioSource.clip = _gameOver;
                break;
            case 2:
                _audioSource.clip = _waveIncoming;
                break;
        }
        _audioSource.volume = 0.2f;
        _audioSource.Play();
    }

}

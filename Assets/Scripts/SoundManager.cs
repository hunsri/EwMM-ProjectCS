using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{

    public static SoundManager soundManager;

    private AudioSource _audioSource;

   // public GameObject infectedPrefab;

    [SerializeField] private AudioClip _coughing, _sneezing;
    [SerializeField] private AudioClip _trainStationBG, _parkBG;

    private void Awake()
    {
        if (soundManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            soundManager = this;
            DontDestroyOnLoad(this);
        }

        string sceneName = SceneManager.GetActiveScene().name;
        
        _audioSource = GetComponent<AudioSource>();
        BgSound(sceneName);

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
    public void Cough(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_coughing, position);    
    }

    void Sneeze(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_sneezing, position);  
    }

    public void BgSound(string sceneName)
    {
        _audioSource.loop = true;
        if (sceneName == "TrainStation")
            _audioSource.clip = _trainStationBG;
        if (sceneName == "ParkWithPlayerHealthbar")
            _audioSource.clip = _parkBG;
        _audioSource.volume = 0.2f;
        _audioSource.Play();   
    }

}

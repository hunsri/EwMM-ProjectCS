using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is just an example script - the method should be used in a script with NPCs behaviour
public class SomeBehaviourScript : MonoBehaviour
{
    private bool soundAgain = false;
    private float timeInterval;
    // Start is called before the first frame update
    void Start()
    {
        SetTimeInterval();
    }

    // Update is called once per frame
    void Update()
    {
        timeInterval -= Time.deltaTime;
       // Debug.Log("timeInterval:" + timeInterval);
        if (timeInterval <= 0) {
            
            PlaySound();
            SetTimeInterval();
        }
    }

    void PlaySound()
    {
        int AudioClipRandom = Random.Range(1, 6);
        Debug.Log("Sound" + AudioClipRandom);
        SoundManager.soundManager.PlayRandomInfectedSounds(AudioClipRandom, transform.position);
    }

    void SetTimeInterval()
    {
        timeInterval = Random.Range(5.0f, 60.0f);
        Debug.Log("Set:" + timeInterval);
    }
}

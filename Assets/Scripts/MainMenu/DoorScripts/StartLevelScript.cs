using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelScript : MonoBehaviour
{
    private int _areYouSureNumber = 0;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetComponent<Animator>();
    }

    // Will load a level
    // Needs to be changed if new level will be added
    public void SelectLevel(int addLevel)
    {
        Debug.Log("Level " + addLevel + " will be started!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + addLevel);
    }

    // Will be called if one of the level door is hitten
    public void TargetReact()
    {
        if (name == "Door Level1")
        {
            AnimatorAndCounter(1);
        }
        else if (name == "Door Level2")
        {
            //AnimatorAndCounter(2);
            Debug.Log("Not implemented!");
        }
        else
        {
            Debug.Log("Something went bad at TargetReact of " + name + " at StartLevelScript");
        }
    }

    // Will activate the right animation
    private void AnimatorAndCounter(int sceneNumber)
    {
        if (_areYouSureNumber == 0)
        {
            _animator.SetTrigger("openHalf");
            _areYouSureNumber++;
        }
        else if (_areYouSureNumber == 1)
        {
            _animator.SetTrigger("openFull");
            SelectLevel(sceneNumber);
        }
        else
        {
            Debug.Log("Something went bad on AnimatorAndCounter() in StartLevelScript!");
        }
    }

    // Will trigger when the game object is triggered
    private void OnTriggerEnter(Collider other)
    {
        TargetReact();
    }
}
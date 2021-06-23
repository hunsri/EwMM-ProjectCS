using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    private int _areYouSureNumber = 0;
    private Animator _animatior;

    // Will be used at the start of the application
    private void Start()
    {
        _animatior = transform.GetComponent<Animator>();
    }

    // Will quit the game
    public void QuitGame()
    {
        if (_areYouSureNumber == 1)
        {
            _animatior.SetTrigger("openFull");
            Debug.Log("Game will quit!");
            Application.Quit();
        }
        else if (_areYouSureNumber == 0)
        {
            _animatior.SetTrigger("openHalf");
            _areYouSureNumber++;
        }
        else
        {
            Debug.Log("Something went bad on QuitGame() in QuitGameScript!");
        }
    }

    // Will trigger when the game object is triggered
    private void OnTriggerEnter(Collider other)
    {
        QuitGame();
    }
}
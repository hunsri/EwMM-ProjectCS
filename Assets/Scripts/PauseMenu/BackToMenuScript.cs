using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
    public class BackToMenuScript : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        private int _areYouSureCounter = 0;

        // Restarts the current level
        private void BackToMenu()
        {
            if (_areYouSureCounter == 0)
            {
                Debug.Log("Are you sure to restart the level?");
                _text.text = "Are you sure?";
                _areYouSureCounter++;
            }
            else if (_areYouSureCounter == 1)
            {
                Debug.Log("This level will be restarted!");
                SceneManager.LoadScene(0);
                _text.text = "Back to menu";
                _areYouSureCounter--;
            }
        }

        // Will trigger when the game object is triggered
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                BackToMenu();
            }
        }
    }
}
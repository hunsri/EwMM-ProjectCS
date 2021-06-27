using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _door;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Restarts the current level
    private void restart()
    {
        Debug.Log("This level will be restarted!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Will trigger when the game object is triggered
    private void OnTriggerEnter(Collider other)
    {
        restart();
    }
}

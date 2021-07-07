using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using NPC;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text _timer;  //represents ui field to show remaining time

    public float timeToWin = 10f;  //time for completing the level
    private bool _timeIsUp;  //bool value to mark when the time is up

    [SerializeField] private GameObject _player;
    public GameObject background;
    public GameObject levelCompleted;
    public GameObject gameOver;
    public GameObject ammoCanvas;

    private NPCWaveManager _waveManager;

    private bool _complete = false;

    // Start is called before the first frame update
    void Start()
    {
        _waveManager = FindObjectOfType<NPCWaveManager>();

        StartCoroutine(SoundManager.soundManager.BgSound());
    }

    // Update is called once per frame
    void Update()
    {
        timeToWin = _waveManager.GetAllRemainingSeconds();

        //if time is up, return
        //for later: !!!IMPORTANT there is need to check wheather the player's health is not 0 AND Level Failed Situation

        float health = _player.GetComponent<PlayerHealth>().getHealth();

        if (_timeIsUp && (health < 1) && !_complete)
        {
            Time.timeScale = 0;
            ShowLevelCompleted();
        }

        if (health == 1 && !_complete)
        {
            Time.timeScale = 0;
            ShowGameOverPanel();
        }

        if (timeToWin <= 0)
        {
            _timeIsUp = true;
        }

        ShowTime(timeToWin);

        if(_complete)
        {
            if(Input.GetButtonDown("Fire1")){
                 SceneManager.LoadScene(0);
                 Time.timeScale =1;
            }
        }
    }

    void ShowTime(float time)
    {
        //convert time into minutes and seconds
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowLevelCompleted()
    {
        SoundManager.soundManager.LevelSounds(0);
        background.SetActive(true);
        levelCompleted.SetActive(true);
        ammoCanvas.SetActive(false);
        _complete = true;
    }

    void ShowGameOverPanel()
    {
        SoundManager.soundManager.LevelSounds(1);
        gameOver.SetActive(true);
        ammoCanvas.SetActive(false);
        _complete = true;
    }

    public bool isComplete{
        get { return _complete; }
    }
}

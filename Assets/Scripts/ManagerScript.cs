using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ManagerScript : MonoBehaviour
{

    [SerializeField] private Text _timer;  //represents ui field to show remaining time

    public float timeToWin = 300f;  //time for completing the level
    private bool _timeIsUp;  //bool value to mark when the time is up

    [SerializeField] private GameObject _player;
    public GameObject background;
    public GameObject levelCompleted;
    public GameObject gameOver;
    public GameObject ammoCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.soundManager.BgSound();
    }

    // Update is called once per frame
    void Update()
    {
        //if time is up, return

        //for later: !!!IMPORTANT there is need to check wheather the player's health is not 0 AND Level Failed Situation

        float health = _player.GetComponent<PlayerHealth>().getHealth();
    
        if (_timeIsUp & health < 1 )
        {
            Time.timeScale = 0;
            ShowLevelCompleted();
        } 

        if(health == 1){
            Time.timeScale = 0;
            ShowGameOverPanel();
        }

        //if there is still time, subtract time from timeToWin till it's <= 0
        timeToWin -= Time.deltaTime;
        if (timeToWin <= 0)
        {
            timeToWin = 0f;
            _timeIsUp = true;
        }

        ShowTime(timeToWin);
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
        background.SetActive(true);
        levelCompleted.SetActive(true);
        ammoCanvas.SetActive(false);
        //for later: Manage LevelCompleted Info
    }

    void ShowGameOverPanel()
    {
        gameOver.SetActive(true);
        ammoCanvas.SetActive(false);
    }

    //timer?
    void blink(){

    }
}

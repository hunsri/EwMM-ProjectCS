using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Data;

namespace MainMenu
{
    public class ScoreScript : MonoBehaviour
    {
        [SerializeField]
        private Text _planeText;

        private PlayerStats _playerStats;

        // Start is called before the first frame update
        void Start()
        {
            _playerStats = new PlayerStats();
            _planeText.GetComponent<Text>();
            _planeText.text = "Score: "+_playerStats.GetPlayerScore() +"\n"+
                                "Healed People: "+ _playerStats.GetCuredNPCs() +"\n"+
                                "XP: "+ _playerStats.GetPlayerXP();
        }

    }
}

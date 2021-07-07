using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Data;

namespace MainMenu
{
    public class RewardAndXPScript : MonoBehaviour
    {
        // Rewards which can be found at the inventory canvas with the headline "Rewards"
        [SerializeField]
        private GameObject _reward1Barricade;
        [SerializeField]
        private GameObject _reward2Barricade;
        [SerializeField]
        private GameObject _reward3Barricade;
        [SerializeField]
        private GameObject _reward4Barricade;
        [SerializeField]
        private GameObject _reward5Barricade;
        [SerializeField]
        private GameObject _reward6Barricade;

        // Shows the correct next item which the player get when he level up
        [SerializeField]
        private GameObject _nextItem1;
        [SerializeField]
        private GameObject _nextItem2;
        [SerializeField]
        private GameObject _nextItem3;

        // Something rewards will de- or activate
        [SerializeField]
        private GameObject _door2Barricade;

        // Is an object of a class which saves data like the XP i need
        private PlayerStats _playerStats;

        // Rank which can be found at the rank canvas with the headline "Rank"
        [SerializeField]
        private Text _textXP;
        [SerializeField]
        private Image _fillImage;
        [SerializeField]
        private Text _currentRankText;
        [SerializeField]
        private Text _nextRankText;
        [SerializeField]
        private Text _currentRankUpperText;

        private int _currentRank;
        private float _currentXP;
        private float _currentMaxXP;

        private int _maxRank = 3;
        private int _maxRank1XP = 10;
        private int _maxRank2XP = 25;
        private int _maxRank3XP = 75;

        [SerializeField]
        private float _newXP = 0;
        [SerializeField]
        private bool _isKlicked = false;

        // Start is called before the first frame update
        void Start()
        {
            _playerStats = new PlayerStats();
            CalculatesXP(_playerStats.GetPlayerXP());
        }

        private void Update()
        {
            if (_isKlicked)
            {
                CalculatesXP(_newXP);
                _isKlicked = false;
            }            
        }

        private void CalculatesXP(float xp)
        {
            if (xp < _maxRank1XP)
            {
                _currentXP = xp;
                _currentRank = 0;
                _currentMaxXP = _maxRank1XP;

                SetTextAndAddProgress();

                _nextItem1.SetActive(true);
                _nextItem2.SetActive(false);
                _nextItem3.SetActive(false);
            }
            else if (xp < _maxRank2XP)
            {
                _currentXP = xp - _maxRank1XP;
                _currentRank = 1;
                _currentMaxXP = _maxRank2XP;

                SetTextAndAddProgress();
                ActivateReward();

                _nextItem1.SetActive(false);
                _nextItem2.SetActive(true);
                _nextItem3.SetActive(false);
            }
            else if (xp < _maxRank3XP)
            {
                _currentXP = xp - _maxRank2XP;
                _currentRank = 2;
                _currentMaxXP = _maxRank3XP;

                SetTextAndAddProgress();
                ActivateReward();

                _nextItem1.SetActive(false);
                _nextItem2.SetActive(false);
                _nextItem3.SetActive(true);
            }
            else
            {
                _currentRank = 3;

                _textXP.text = "Max Rank";
                _currentRankText.text = _maxRank + "";
                _currentRankUpperText.text = "Rank " + _maxRank;
                _nextRankText.text = _currentRank + "";
                _fillImage.fillAmount = 1;

                ActivateReward();

                _nextItem3.SetActive(false);
                Debug.Log("You reached max rank.");
            }
        }

        /// <summary>
        /// Adds the new XP to the rank canvas
        /// </summary>
        private void SetTextAndAddProgress()
        {
            float fillCalculation = _currentXP / _currentMaxXP;
            int nextRank = _currentRank + 1;

            _textXP.text = _currentXP + "/" + _currentMaxXP + " XP";
            _fillImage.fillAmount = fillCalculation;

            _currentRankText.text = _currentRank + "";
            _currentRankUpperText.text = "Rank " + _currentRank;
            _nextRankText.text = nextRank + "";
        }

        /// <summary>
        /// Will deactivate the barricades of the rewards and activate them
        /// </summary>
        private void ActivateReward()
        {
            if (_currentRank == 1)
            {
                // Reward is to play the second level
                _reward1Barricade.SetActive(false);
                _door2Barricade.SetActive(false);
            }
            else if (_currentRank == 2)
            {
                _reward1Barricade.SetActive(false);
                _door2Barricade.SetActive(false);

                _reward2Barricade.SetActive(false);
                // Add more resistance
            }
            else if (_currentRank == 3)
            {
                _reward1Barricade.SetActive(false);
                _door2Barricade.SetActive(false);

                _reward2Barricade.SetActive(false);
                // Add more resistance

                _reward3Barricade.SetActive(false);
                // Add more ammunition
            }
            else if (_currentRank == 0)
            {
                Debug.Log("Something went wrong on ActivateReward() in RewardAndXPScript!");
            }
            else
            {
                Debug.Log("More ranks are not implemented yet.");
            }
        }
    }
}
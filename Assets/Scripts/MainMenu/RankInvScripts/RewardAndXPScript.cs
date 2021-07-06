using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Rank which can be found at the rank canvas with the headline "Rank"
    // Needs to be saved
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

    // Will be added through the score
    [SerializeField]
    private int _xp;

    // Needs to be saved
    private float _currentMaxXP;
    private int _currentRank;
    private int _currentXP;

    private int _maxRank = 3;
    private int _maxRank1XP = 10;
    private int _maxRank2XP = 25;
    private int _maxRank3XP = 75;
    [SerializeField]
    private bool _rankNeedsToBeChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        // TODO - Load XP data 
        // Needs to be linked to the permanent save
        _xp = 0;
        _currentMaxXP = _maxRank1XP;
        _nextItem1.SetActive(true);
        _nextItem2.SetActive(false);
        _nextItem3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_rankNeedsToBeChanged)
        {
            SetTextAndAddProgress(_xp);
            _rankNeedsToBeChanged = false;
        }
    }

    private void CalculatesXP()
    {
        // Needs score
        _rankNeedsToBeChanged = true;
    }

    /// <summary>
    /// Adds the new XP to the rank canvas
    /// </summary>
    /// <param name="xp"></param>
    private void SetTextAndAddProgress(float xp)
    {
        // For the text which shows the XP
        int localCalculation1 = (int)(_currentXP + xp);
        // For the fill image
        float localCalculation2 = xp / _currentMaxXP;

        if (_currentRank != _maxRank)
        {
            // if 8/10 = true, else 16/10 false
            if (localCalculation1 < _currentMaxXP)
            {
                _textXP.text = localCalculation1 + "/" + _currentMaxXP + " XP";
                _fillImage.fillAmount += localCalculation2;

                _currentXP = localCalculation1;
            }
            else if (localCalculation1 >= _currentMaxXP)
            {
                LevelUp(localCalculation1);
            }
            else
            {
                Debug.Log("Something went bad on SetTextAndAddProgress() in RewardAndXPScript!");
            }
        }
        else
        {
            Debug.Log("You reached max rank.");
        }
    }

    /// <summary>
    /// Will change parts of the canvas if you reach a new rank
    /// </summary>
    /// <param name="xp"></param>
    private void LevelUp(int xp)
    {
        _currentRank++;

        if (_currentRank != _maxRank)
        {
            // If you got 8 XP but you already have 8/10 XP, your 6 XP should not vanish
            int overflow = (int)(xp - _currentMaxXP);
            _currentXP = overflow;

            // Sets the new XP limit
            if (_currentRank == 1)
            {
                _currentMaxXP = _maxRank2XP;
                _nextItem1.SetActive(false);
                _nextItem2.SetActive(true);
            }
            else if (_currentRank == 2)
            {
                _currentMaxXP = _maxRank3XP;
                _nextItem2.SetActive(false);
                _nextItem3.SetActive(true);
            }
            else
            {
                Debug.Log("Something went wrong on LevelUp() in RewardAndXPScript!");
            }

            int nextRank = _currentRank + 1;

            // For the fill image
            float flowCalculation = overflow / _currentMaxXP;

            // Shows the new limit of XP and adds the overflow
            _textXP.text = overflow + "/" + _currentMaxXP + " XP";
            _fillImage.fillAmount = flowCalculation;

            // Shows your new rank. Congrats!
            _currentRankText.text = _currentRank + "";
            _currentRankUpperText.text = "Rank " + _currentRank;
            _nextRankText.text = nextRank + "";

            ActivateReward();
        }
        else
        {
            _textXP.text = "Max Rank";
            _currentRankText.text = _maxRank + "";
            _currentRankUpperText.text = "Rank " + _maxRank;
            _nextRankText.text = _currentRank + "";
            _fillImage.fillAmount = 1;

            _nextItem3.SetActive(false);

            _currentXP = (int)_currentMaxXP;
            Debug.Log("You reached max rank.");
        }
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
            _reward2Barricade.SetActive(false);
        }
        else if (_currentRank == 3)
        {
            _reward3Barricade.SetActive(false);
        }
        else if (_currentRank == 0)
        {
            Debug.Log("Something went wrong on ActivateReward() in RewardAndXPScript!");
        }
        else
        {
            Debug.Log("Not implemented yet.");
        }
    }
}

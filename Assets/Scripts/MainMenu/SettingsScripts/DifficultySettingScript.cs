using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _easyPlane;
    [SerializeField]
    private GameObject _mediumPlane;
    [SerializeField]
    private GameObject _hardPlane;

    private MeshRenderer _easyRenderer;
    private MeshRenderer _mediumRenderer;
    private MeshRenderer _hardRenderer;

    private int _difficulty = 0;
    private int _minDif = 0;
    private int _maxDif = 2;

    private string _shaderPropertyName = "_Color";
    private Color _colorWhite = Color.white;
    private Color _colorBlack = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        _easyRenderer = _easyPlane.GetComponent<MeshRenderer>();
        _easyRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
        _mediumRenderer = _mediumPlane.GetComponent<MeshRenderer>();
        _mediumRenderer.material.SetColor(_shaderPropertyName, _colorWhite);
        _hardRenderer = _hardPlane.GetComponent<MeshRenderer>();
        _hardRenderer.material.SetColor(_shaderPropertyName, _colorWhite);
    }

    // Update is called once per frame
    void Update()
    {
        // Curently not needed
    }

    // Changes the difficulty of all levels
    public void SetDifficulty()
    {
        // TODO
    }

    // Changes the color of the planes which are showing the difficulty
    public void PlaneColorChange()
    {
        if (_difficulty == 0)
        {
            _easyRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
            _mediumRenderer.material.SetColor(_shaderPropertyName, _colorWhite);
            _hardRenderer.material.SetColor(_shaderPropertyName, _colorWhite);
        }
        else if (_difficulty == 1)
        {
            _easyRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
            _mediumRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
            _hardRenderer.material.SetColor(_shaderPropertyName, _colorWhite);
        }
        else if (_difficulty == 2)
        {
            _easyRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
            _mediumRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
            _hardRenderer.material.SetColor(_shaderPropertyName, _colorBlack);
        }
        else
        {
            Debug.Log("Something went bad on planeColorChange in DifficultySettingScript!");
        }
    }

    // Gets the color of the plane
    // Evoids miscoloration
    // Returns an int which indicates the difficulty
    public int GetDifficultyThroughPlaneColor()
    {
        if (_hardRenderer.material.GetColor(_shaderPropertyName) == _colorBlack)
        {
            return 2;
        }
        else if (_mediumRenderer.material.GetColor(_shaderPropertyName) == _colorBlack)
        {
            return 1;
        }
        else if (_easyRenderer.material.GetColor(_shaderPropertyName) == _colorBlack)
        {
            return 0;
        } else
        {
            Debug.Log("Something went bad on getDifficultyThroughPlaneColor() in DifficultySettingScript!");
            return 4;
        }
    }

    // Will be used when the arrows next to the difficulty are triggered
    public void TargetReact()
    {
        _difficulty = GetDifficultyThroughPlaneColor();

        if (name == "Left Arrow Difficulty Button")
        {
            if (_difficulty != _minDif)
            {
                _difficulty--;
                SetDifficulty();
                PlaneColorChange();
            } 
            else
            {
                Debug.Log("The difficulty is already as low as it can be!");
            }
        } 
        else if (name == "Right Arrow Difficulty Button")
        {
            if (_difficulty != _maxDif)
            {
                _difficulty++;
                SetDifficulty();
                PlaneColorChange();
            } 
            else
            {
                Debug.Log("The difficulty is already as high as it can be!");
            }
        } 
        else if (name == "Difficulty Easy Plane")
        {
            if (_difficulty != _minDif)
            {
                _difficulty = 0;
                SetDifficulty();
                PlaneColorChange();
            }
            else
            {
                Debug.Log("The difficulty is already easy!");
            }
        }
        else if (name == "Difficulty Medium Plane")
        {
            if (_difficulty != 1)
            {
                _difficulty = 1;
                SetDifficulty();
                PlaneColorChange();
            }
            else
            {
                Debug.Log("The difficulty is already medium!");
            }
        } 
        else if (name == "Difficulty Hard Plane")
        {
            if (_difficulty != _maxDif)
            {
                _difficulty = 2;
                SetDifficulty();
                PlaneColorChange();
            }
            else
            {
                Debug.Log("The difficulty is already hard!");
            }
        }
    }
}
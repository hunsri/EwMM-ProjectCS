using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class CompleteLevelStats : MonoBehaviour
{
    [SerializeField] private Text _text;

    private SceneStats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _stats = FindObjectOfType<SceneStats>();
        _text.GetComponent<Text>();
    }

    void Update(){
        _text.text = "Cured: " + _stats.GetCured() +"\n" +
                     "Score: " + _stats.GetScore() +"\n" +"\n" + 

                     "You have gained " + _stats.GetEXP() + " EXP !";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class MinimapScript : MonoBehaviour
{
    public GameObject minimap;    
    public Text closeText;
    public Text keyText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("tab"))
        {
            if (minimap.activeInHierarchy)
            {
                HideMinimap();
            }
            else ShowMinimap();
        }
    }

    void ShowMinimap()
    {
        minimap.SetActive(true);
        closeText.text = "CLOSE MAP";
        
    }

    void HideMinimap()
    {
        minimap.SetActive(false);        
        closeText.text = "OPEN MAP";

    }
}

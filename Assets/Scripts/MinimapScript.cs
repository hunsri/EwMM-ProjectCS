using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject minimap;

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
    }

    void HideMinimap()
    {
        minimap.SetActive(false);
    }
}

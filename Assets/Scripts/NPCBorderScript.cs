using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBorderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        //disables the MeshRenderer so that the object appears invisible
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

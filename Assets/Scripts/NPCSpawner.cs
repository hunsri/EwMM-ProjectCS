using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _npc;

    // Start is called before the first frame update
    void Start()
    {
    Instantiate(_npc, this.transform.position, Quaternion.Euler(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

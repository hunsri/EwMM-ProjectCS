using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankAndInventoryScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _rankCanvas;
    [SerializeField]
    private GameObject _inventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _rankCanvas.SetActive(true);
        _inventoryCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_rankCanvas.activeSelf == true)
        {
            changeCanvas(_inventoryCanvas);
        }
        else if (_inventoryCanvas.activeSelf == true)
        {
            changeCanvas(_rankCanvas);
        }
        else
        {
            Debug.Log("Something went bad on OnTriggerEnter() in ArchivementAndRewardsScript!");
        }
    }

    // Changes so that the given gameObject is now active an the other one are not
    public void changeCanvas(GameObject gameObject)
    {
        if (gameObject.name == _rankCanvas.name)
        {
            _rankCanvas.SetActive(true);
            _inventoryCanvas.SetActive(false);
        }
        else if (gameObject.name == _inventoryCanvas.name)
        {
            _rankCanvas.SetActive(false);
            _inventoryCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("Something went bad on changeCanvas() in ArchivementAndRewardsScript!");
            _rankCanvas.SetActive(false);
            _inventoryCanvas.SetActive(false);
        }
    }
}

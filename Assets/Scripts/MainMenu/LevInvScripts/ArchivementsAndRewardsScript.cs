using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivementsAndRewardsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _rewardCanvas;
    [SerializeField]
    private GameObject _archivementsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _rewardCanvas.SetActive(true);
        _archivementsCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_rewardCanvas.activeSelf == true)
        {
            changeCanvas(_archivementsCanvas);
        }
        else if (_archivementsCanvas.activeSelf == true)
        {
            changeCanvas(_rewardCanvas);
        }
        else
        {
            Debug.Log("Something went bad on OnTriggerEnter() in ArchivementAndRewardsScript!");
        }
    }

    // Changes so that the given gameObject is now active an the other one are not
    public void changeCanvas(GameObject gameObject)
    {
        if (gameObject.name == _rewardCanvas.name)
        {
            _rewardCanvas.SetActive(true);
            _archivementsCanvas.SetActive(false);
        }
        else if (gameObject.name == _archivementsCanvas.name)
        {
            _rewardCanvas.SetActive(false);
            _archivementsCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("Something went bad on changeCanvas() in ArchivementAndRewardsScript!");
            _rewardCanvas.SetActive(false);
            _archivementsCanvas.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private PlayerData _data;

    // Start is called before the first frame update
    void Start()
    {
        _data = new PlayerData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Get a specific weapon by its index. Returns null if weapon with provided index
    /// is not found
    /// </summary>
    public WeaponData GetWeapon(int weaponIndex)
    {
        foreach (WeaponData weaponData in _data.WeaponDatas)
        {
            if (weaponData.GetWeaponIndex() == weaponIndex)
            {
                return weaponData;
            }
        }

        return null;
    }

    /// <summary>
    /// Get list of available weapons
    /// </summary>
    public WeaponData[] GetAllWeapons()
    {
        return _data.WeaponDatas;
    }

    /// <summary>
    /// Load resource from `/Resources` folder at runtime
    /// </summary>
    public void LoadResource(string filename, Transform parent)
    {
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load(filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }

        GameObject go = Instantiate(loadedObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.SetParent(parent.transform);
        go.transform.position = parent.position;
    }
}

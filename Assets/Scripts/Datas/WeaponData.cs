[System.Serializable]
public class WeaponData
{
    private int _weaponIndex;
    private int _ammoCount;
    private float[] _defaultPosition;
    private string _weaponName;

    /// <summary>
    /// weaponIndex: Index of the weapon
    /// maxAmmo: Max ammo of the weapon
    /// defaultPosition: Position of the weapon on render (useful to "snap" weapon to its holder)
    /// </summary>
    public WeaponData(int weaponIndex, int maxAmmo, float[] defaultPosition, string weaponName)
    {
        _weaponIndex = weaponIndex;
        _ammoCount = maxAmmo;
        _defaultPosition = defaultPosition;
        _weaponName = weaponName;
    }

    public int GetAmmoCount()
    {
        return _ammoCount;
    }

    public void SetAmmoCount(int newAmmoCount)
    {
        _ammoCount = newAmmoCount;
    }

    public int GetWeaponIndex()
    {
        return _weaponIndex;
    }

    public float[] GetDefaultPosition()
    {
        return _defaultPosition;
    }
}
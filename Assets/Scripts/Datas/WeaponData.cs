[System.Serializable]
public class WeaponData
{
    private int _weaponIndex;
    private int _ammoCount;

    public WeaponData(int weaponIndex, int maxAmmo)
    {
        _weaponIndex = weaponIndex;
        _ammoCount = maxAmmo;
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
}
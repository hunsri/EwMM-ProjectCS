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

    public void IncreaseAmmo(int increaseBy)
    {
        SetAmmoCount(_ammoCount += increaseBy);
    }

    public void DecreaseAmmo(int decreaseBy)
    {
        SetAmmoCount(_ammoCount -= decreaseBy);
    }

    public int GetWeaponIndex()
    {
        return _weaponIndex;
    }
}
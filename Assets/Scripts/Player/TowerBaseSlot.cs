using UnityEngine;

public class TowerBaseSlot : MonoBehaviour {
    public bool isOccupied ;
    [SerializeField] private Transform mountPoint;
    private int currentLvl = 1;   
    private WeaponData weaponData;
    private WeaponBase curWeaponBase;
    private GameObject weapon;

    public int CurrentLvl => currentLvl;

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public bool CanUpgrade()
    {
        if (!isOccupied) return false;

        return GoldManager.Instance.CanAfford(weaponData.GetUpgradeCost(currentLvl));
    }

    public Vector3 GetMountPosition()
    {
        return mountPoint != null ? mountPoint.position : transform.position;
    }
    public void PlaceWeapon(WeaponData data)
    {
        if (!GoldManager.Instance.CanAfford(data.baseCost)) return;

        if (isOccupied) return;

        GoldManager.Instance.Spend(data.baseCost);
        weaponData = data;
        weapon = Instantiate(data.levelPrefabs[0], GetMountPosition(), Quaternion.Euler(0,270,0));
        curWeaponBase = weapon.GetComponent<WeaponBase>();
        isOccupied = true;
        currentLvl = 1;
    }

    public void TryUpgrade()
    {
        if (!CanUpgrade())
        {
            Debug.Log("ko du vang");
            return;
        }

        Debug.Log("Success");
        GoldManager.Instance.Spend(weaponData.GetUpgradeCost(currentLvl));
        
        currentLvl++;
        curWeaponBase.ApplyStat(weaponData,currentLvl);
                
    }

    public void RemoveWeapon()
    {
        if (!isOccupied) return;

        int refund = weaponData.GetSellValue(currentLvl);
        GoldManager.Instance.Add(refund);

        Destroy(weapon);
        weapon = null;
        curWeaponBase = null;
        weaponData = null;
        isOccupied = false;
        currentLvl = 1;
    }
}
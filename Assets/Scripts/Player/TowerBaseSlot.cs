using UnityEngine;

public class TowerBaseSlot : MonoBehaviour {
    public bool isOccupied ;
    [SerializeField] private Transform mountPoint;
    [SerializeField] private int gold = 1000;
    private int currentLvl;   
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

        return gold >= weaponData.GetUpgradeCost(currentLvl);
    }

    public Vector3 GetMountPosition()
    {
        return mountPoint != null ? mountPoint.position : transform.position;
    }
    public void PlaceWeapon(WeaponData data)
    {
        if (isOccupied) return;
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

        Debug.Log("Success upgrade");
        currentLvl++;
        curWeaponBase.ApplyStat(weaponData,currentLvl);
        Debug.Log(weaponData.GetDamage(currentLvl) + "\n" + weaponData.GetFireRate(currentLvl));
                
    }
}
using UnityEngine;

public class TowerBaseSlot : MonoBehaviour {
    public bool isOccupied;
    [SerializeField] private Transform mountPoint;
    [SerializeField] private AudioClip chooseSfx;
    private int currentLvl = 1;
    private WeaponData weaponData;
    private WeaponBase curWeaponBase;
    private GameObject weapon;

    public int CurrentLvl => currentLvl;

    public WeaponData GetWeaponData() => weaponData;

    public bool CanUpgrade()
    {
        if (!isOccupied) return false;
        return GoldManager.Instance.CanAfford(weaponData.GetUpgradeCost(currentLvl));
    }

    public Vector3 GetMountPosition() => mountPoint != null ? mountPoint.position : transform.position;

    void Update()
    {
        if (!isOccupied) return;

        Transform target = curWeaponBase.CurrentTarget;
        if (target != null)
        {
            RotateMountTowardsTarget(target);
        }
    }

    void RotateMountTowardsTarget(Transform target)
    {
        Transform rotTransform = mountPoint != null ? mountPoint : transform;
        Vector3 dir = target.position - rotTransform.position;
        if (dir.sqrMagnitude < 0.001f) return;

        rotTransform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90,0,0);
    }

    public void PlaceWeapon(WeaponData data)
    {
        if (!GoldManager.Instance.CanAfford(data.baseCost)) return;
        if (isOccupied) return;
        AudioManager.Instance.PlaySFX(chooseSfx);

        GoldManager.Instance.Spend(data.baseCost);
        weaponData = data;

        weapon = Instantiate(data.levelPrefabs[0], GetMountPosition(), Quaternion.Euler(0, 270, 0));
        weapon.transform.SetParent(mountPoint != null ? mountPoint : transform);

        curWeaponBase = weapon.GetComponent<WeaponBase>();
        curWeaponBase.ApplyStat(data, 1);
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
        curWeaponBase.ApplyStat(weaponData, currentLvl);
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
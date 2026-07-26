using UnityEngine;

public enum weaponTyoe  { Gatling, Grenade, Plasma, Sniper}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject {
    public string weaponName;
    public weaponTyoe weaponTyoe;
    public GameObject[] levelPrefabs = new GameObject[3];
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;
    public float baseDamage = 10f;
    public float baseFireRate = 1f;

    public float damageGrowth = 1.3f;
    public float fireRateGrowth = 1.15f;

    public int baseCost = 50;
    public float costGrowth = 1.5f;

    public float plasmaDuration = 0.3f;
    public float grenadeSpinSpeed = 720f;

    public int maxLevel = 3;

    public float GetDamage(int level) => baseDamage * Mathf.Pow(damageGrowth, level - 1);
    public float GetFireRate(int level) => baseFireRate * Mathf.Pow(fireRateGrowth, level - 1);
    public int GetUpgradeCost(int currentLevel) => Mathf.RoundToInt(baseCost * Mathf.Pow(costGrowth, currentLevel - 1));

    public int GetSellValue(int lvl) => Mathf.RoundToInt(baseCost * lvl * 0.2f);
    public GameObject GetPrefab(int level) => levelPrefabs[Mathf.Clamp(level - 1, 0, levelPrefabs.Length - 1)];
}

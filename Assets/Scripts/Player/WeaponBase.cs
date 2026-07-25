using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private float damage;
    private float fireRate;

    public void ApplyStat(WeaponData data, int level)
    {
        weaponData = data;
        damage = data.GetDamage(level);
        fireRate = data.GetFireRate(level);
    }
}

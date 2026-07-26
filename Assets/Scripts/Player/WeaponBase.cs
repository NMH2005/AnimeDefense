using System;
using UnityEngine;

public class WeaponBase : MonoBehaviour {
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange = 10f;
    private float damage;
    private float fireRate;
    private float fireTimer;

    public void ApplyStat(WeaponData data, int level)
    {
        weaponData = data;
        damage = data.GetDamage(level);
        fireRate = data.GetFireRate(level);
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0)
        {
            Transform target = FindTarget();
            if (target != null)
            {
                Fire(target);
                fireTimer = 1f / fireRate;
            }
        }
    }



    private Transform FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(firePoint.position, detectRange, enemyLayer);
        Transform closetEnemy = null;
        float closetDis = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dis = Vector3.Distance(firePoint.position, hit.transform.position);
            if (dis < closetDis)
            {
                closetDis = dis;
                closetEnemy = hit.transform;
            }
        }

        return closetEnemy;
    }

    private void Fire(Transform target)
    {
        Vector3 dir = (target.position - firePoint.position).normalized;

        GameObject ammoObj = Instantiate(weaponData.bulletPrefab, firePoint.position, Quaternion.identity);

        switch(weaponData.weaponTyoe)
        {
            case weaponTyoe.Gatling:
            case weaponTyoe.Sniper:
                ammoObj.GetComponent<Bullet>().Init(damage, weaponData.bulletSpeed);
                break;
            case weaponTyoe.Grenade:
                ammoObj.GetComponent<Grenade>().Init(damage, weaponData.bulletSpeed, weaponData.grenadeSpinSpeed);
                break;
            case weaponTyoe.Plasma:
                ammoObj.GetComponent<Plasma>().Init(damage, weaponData.plasmaDuration);
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (firePoint != null)
            Gizmos.DrawWireSphere(firePoint.position, detectRange);
    }
}

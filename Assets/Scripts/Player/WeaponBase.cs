using UnityEngine;

public class WeaponBase : MonoBehaviour {
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange = 15f;

    private int currentLevel = 1;
    private float damage;
    private float fireRate;
    private float fireTimer;

    public void ApplyStat(WeaponData data, int level)
    {
        weaponData = data;
        currentLevel = level;
        damage = data.GetDamage(level);
        fireRate = data.GetFireRate(level);
        fireTimer = 1f / fireRate;
    }
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Transform target = FindTarget();
            if (target != null)
            {
                Fire(target);
                fireTimer = 1f / fireRate;
            }
        }
    }

    Transform FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(firePoint.position, detectRange, enemyLayer);
        Transform closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(firePoint.position, hit.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = hit.transform;
            }
        }

        return closest;
    }

    void Fire(Transform target)
    {
        Vector3 dir = target.position - firePoint.position;
        dir.y = 0f;
        dir.Normalize();

        GameObject obj = Instantiate(weaponData.bulletPrefab, firePoint.position, Quaternion.identity);

        switch (weaponData.weaponTyoe)
        {
            case weaponTyoe.Gatling:
            case weaponTyoe.Sniper:
                obj.GetComponent<Bullet>().Init(damage, weaponData.bulletSpeed, dir);
                break;

            case weaponTyoe.Grenade:
                obj.GetComponent<Grenade>().Init(damage, weaponData.bulletSpeed, dir, weaponData.grenadeSpinSpeed);
                break;

            case weaponTyoe.Plasma:
                obj.GetComponent<Plasma>().Init(damage, weaponData.plasmaDuration, dir);
                break;
        }
    }
}

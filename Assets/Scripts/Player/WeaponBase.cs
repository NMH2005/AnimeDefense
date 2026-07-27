using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponBase : MonoBehaviour {
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange = 15f;

    private int currentLevel = 1;
    private float damage;
    private float fireRate;
    private float fireTimer;
    private GameObject activePlasmaObj;
    public Transform CurrentTarget { get; private set; }

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
        Transform target = FindTarget();
        CurrentTarget = target;
        if (weaponData.weaponTyoe == weaponTyoe.Plasma)
        {
            HandlePlasma(target);
            return;
        }

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            if (target != null)
            {
                Fire(target);
                fireTimer = 1f / fireRate;
            }
        }
    }


    void HandlePlasma(Transform target)
    {
        if (target == null)
        {
            if (activePlasmaObj != null)
            {
                Destroy(activePlasmaObj);
                activePlasmaObj = null;
            }
            return;
        }

        if (activePlasmaObj == null)
        {
                Vector3 dir = target.position - firePoint.position;
            dir.y = 0f;
            dir.Normalize();

            activePlasmaObj = Instantiate(weaponData.bulletPrefab, firePoint.position, Quaternion.identity);
            activePlasmaObj.GetComponent<Plasma>().Init(damage, weaponData.bulletSpeed, dir, target);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(weaponData.fireSFX);
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

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(weaponData.fireSFX);
        }

        switch (weaponData.weaponTyoe)
        {
            case weaponTyoe.Gatling:
            case weaponTyoe.Sniper:
                obj.GetComponent<Bullet>().Init(damage, weaponData.bulletSpeed, dir);
                break;

            case weaponTyoe.Grenade:
                obj.GetComponent<Grenade>().Init(damage, weaponData.bulletSpeed, dir, weaponData.grenadeSpinSpeed);
                break;
        }
    }
}

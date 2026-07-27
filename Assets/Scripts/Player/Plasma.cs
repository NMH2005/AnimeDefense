using System;
using UnityEngine;

public class Plasma : Bullet {
    [SerializeField] private float maxBeamLength = 15f;
    [SerializeField] private LayerMask enemyLayer;
    private float damagePerTick;
    [SerializeField] private float tickInterval = 400f;
    private float tickTimer;
    private float originalScaleX;
    private Transform target;

    public void Init(float dmg, float spd, Vector3 dir, Transform targetTransform)
    {
        base.Init(dmg, spd, dir);
        damagePerTick = dmg;
        tickTimer = 0f;
        originalScaleX = transform.localScale.x;
        target = targetTransform;

        UpdateBeamVisual();
    }

    protected override void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        direction = (target.position - transform.position);
        direction.y = 0f;
        direction.Normalize();
        UpdateRotation();
        UpdateBeamVisual();

        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            DealDamageTick();
            tickTimer = tickInterval;
        }
    }

    void UpdateRotation()
    {
        Quaternion faceCam = Camera.main.transform.rotation * Quaternion.Euler(90, 0, 0);
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = faceCam * Quaternion.Euler(90f, angle, 0f);
    }
    protected override void HandleTriggerHit(IDamageable target)
    {

    }


    void UpdateBeamVisual()
    {
        float hitDistance = maxBeamLength;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxBeamLength, enemyLayer))
        {
            hitDistance = hit.distance;
        }

        Vector3 scale = transform.localScale;
        scale.x = originalScaleX * (hitDistance / maxBeamLength);
        transform.localScale = scale;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }

    void DealDamageTick()
    {

        if (target != null && target.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damagePerTick);
        }
    }
}
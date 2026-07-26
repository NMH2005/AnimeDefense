using UnityEngine;

public class Plasma : Bullet {
    [SerializeField] private float maxBeamLength = 15f;

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

        UpdateBeamVisual();

        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            DealDamageTick();
            tickTimer = tickInterval;
        }
    }

    void UpdateBeamVisual()
    {
        float hitDistance = maxBeamLength;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxBeamLength))
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
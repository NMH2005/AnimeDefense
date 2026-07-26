using UnityEngine;

public class Plasma : Bullet
{
    public override void Init(float damage, float duration, Vector3 dir)
    {
        float beamLength = 15f;
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, beamLength))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var target))
                target.TakeDamage(damage);
        }

        Destroy(gameObject, duration);
    }
}

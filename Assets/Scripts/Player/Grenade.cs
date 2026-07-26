using System;
using UnityEngine;

public class Grenade : Bullet {

    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private LayerMask enemyLayer;

    float spinSpeed;

    public void Init(float dmg, float spd, Vector3 dir, float spin)
    {
        base.Init(dmg, spd, dir);
        spinSpeed = spin;
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime, Space.Self);
    }

    protected override void OnHit()
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach (var hit in hits)
        {
            if(hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(damage);
            }
        }
    }
}


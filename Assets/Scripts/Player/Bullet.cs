using UnityEngine;

public class Bullet : MonoBehaviour {
    protected float damage;
    protected float speed;
    protected Vector3 direction;

    public virtual void Init(float dmg, float spd, Vector3 dir)
    {
        damage = dmg;
        speed = spd;
        direction = dir.normalized;

        Quaternion faceCam = Camera.main.transform.rotation * Quaternion.Euler(90, 0, 0);

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = faceCam * Quaternion.Euler(90f,angle ,0f);

        Destroy(gameObject, 10f);
    }

    protected virtual void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
                target.TakeDamage(damage);
                OnHit();
        }
    }

    protected virtual void OnHit() => Destroy(gameObject);
}
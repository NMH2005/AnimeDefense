using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float damage;
    protected float speed;

    public virtual void Init(float dmg, float spd)
    {
        damage = dmg;
        speed = spd;
    }

    protected virtual void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) {
            collision.TryGetComponent<IDamageable>(out var target);
            target.TakeDamage(damage);
            OnHit();
       }    
    }

    protected virtual void OnHit()
    {
        Destroy(gameObject);
    }
}

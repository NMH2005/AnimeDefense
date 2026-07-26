using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealthBar healthBar;

    private Transform target;
    private float speed;
    private float maxHealth;
    private int value;
    private float currentHealth;

    public void Initialize(Transform target, float spd, float maxHealth, int val )
    {
        this.target = target;
        this.speed = spd;
        this.maxHealth = maxHealth;
        this.value = val;
        this.currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GoldManager.Instance.Add(value);
        Destroy(gameObject);
    }
}
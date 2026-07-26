using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed = 3f;
    private Transform target;
    private Animator animator;
    private float currentHealth;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(Transform target)
    {
        this.target = target;
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
        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

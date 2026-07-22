using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Transform target;
    
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
}

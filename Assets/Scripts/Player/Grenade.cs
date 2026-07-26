using System;
using UnityEngine;

public class Grenade : Bullet {

    float spinSpeed;
    public void Init(float damage, float bulletSpeed, float grenadeSpinSpeed)
    {
        base.Init(damage,bulletSpeed);
        spinSpeed = grenadeSpinSpeed;
    }

    protected override void Update()
    {
        base .Update();
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime, Space.Self);
    }

    protected override void OnHit()
    {
        Destroy(gameObject);
    }
}


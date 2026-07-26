using System;
using UnityEngine;

public class Grenade : Bullet {

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
        Destroy(gameObject);
    }
}


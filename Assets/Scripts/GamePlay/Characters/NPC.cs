using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPC : TemproryObject
{
    public int Health;
    public float Speed;
    public float RotationSpeed;

    public UnityEvent OnDeath;

    protected bool Dead;

    public virtual void Move(Vector2 Location)
    {
        LookAt(Location);
        transform.Translate(new Vector2(0, -Speed) * Time.deltaTime);
    }
    public virtual void LookAt(Vector2 Location)
    {
        transform.Rotate(0, 0, (-CrossRotation(Location, transform) * (RotationSpeed * 2.5f)));
    }
    public virtual void TakeDamage(int DamageMount)
    {
        if (Dead || DamageMount <= 0)
            return;

        Health -= DamageMount;

        if (Health <= 0)
            Die();

    }
    public virtual void Die()
    {
        Dead = true;
        OnDeath.Invoke();
    }

    protected override void CheckVisible()
    {
        if (Dead)
            base.CheckVisible();
    }

    float CrossRotation(Vector2 Target, Transform GameObject)
    {
        Vector2 direction = Target - (Vector2)GameObject.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, GameObject.up).z;

        return -rotateAmount;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cockroach : TemproryObject 
{
	public enum ChaseType
	{
		Forward,Zigzag
	}

	public ChaseType MoveType;

	public int Health;

	public Transform Target;

	public float Speed;
	public float RotationSpeed;

	public float MinSize;
	public float MaxSize;

	public UnityEvent OnCatchTarget;
	public UnityEvent OnDeath;

    protected bool Dead;
	protected Animator animator;

	int Zigzagdomain = 4;
    uint x;
	sbyte i;

    protected override void Start () 
    {
		base.Start ();

		float size = UnityEngine.Random.Range(MinSize, MaxSize);
		transform.localScale = new Vector3(size, size, size);

		if(!Target)
			Target = GameController.instance.PlayerPosition;


		Speed = GameController.instance.EnemiesSpeed;

		animator = GetComponent<Animator>();

		GameController.instance.cockroaches.Add(this);
    }
	protected override void Update () 
    {
        base.Update();

        if (Target && !Dead)
            ChaseTarget(Target);
    }
    protected override void CheckVisible()
    {
		if(Dead)
			base.CheckVisible();
    }

    protected virtual void ChaseTarget(Transform target)
	{
		float Distance = Vector2.Distance(transform.position, target.position);

		if (Distance <= 0.1f)
		{
			GameController.instance.GameOver();
			OnCatchTarget.Invoke();
			return;
		}

		Vector2 TargetLocation = target.position;


		switch (MoveType)
		{
			case ChaseType.Zigzag:

                i++;

                if (i % 100 == 0)
                {
                    x++;
                    i = 0;
                }

                TargetLocation = new Vector2(target.position.x + (Mathf.Sin(x) * Zigzagdomain), target.position.y + (Mathf.Sin(x) * Zigzagdomain));
                break;

				default : break;
		}

        transform.Rotate(0, 0, (-CrossRotation(TargetLocation, transform) * (RotationSpeed * 2.5f)));

        transform.Translate(new Vector2(0, -Speed) * Time.deltaTime);
    }

	public virtual void TakeDamage(int DamageMount)
	{
		if (Dead || DamageMount <= 0)
			return;

        Health-=DamageMount;

		if (Health <= 0)
			Die();

    }
	public virtual void Die()
	{
		Dead = true;

        animator.SetBool("Dead",true);

        GameController.instance.cockroaches.Remove(this);
		GameController.instance.OnEnemyDie.Invoke();

		GetComponent<CircleCollider2D>().enabled = false;
        OnDeath.Invoke();
    }

    float CrossRotation(Vector2 Target, Transform GameObject)
    {
        Vector2 direction = Target - (Vector2)GameObject.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, GameObject.up).z;

        return -rotateAmount;
    }
}

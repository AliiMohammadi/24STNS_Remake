using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cockroach : NPC 
{
	public enum ChaseType
	{
		Forward,Zigzag
	}

	public ChaseType MoveType;

	public Transform Target;

	public float MinSize;
	public float MaxSize;

	public UnityEvent OnCatchTarget;

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
		Move(TargetLocation);
    }

	public override void Die()
	{
        animator.SetBool("Dead",true);

        GameController.instance.cockroaches.Remove(this);
		GameController.instance.OnEnemyDie.Invoke();

		GetComponent<CircleCollider2D>().enabled = false;

        GetComponent<SpriteRenderer>().sortingOrder = 0;

        base.Die();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cockroach : TemproryObject 
{
	public Transform Target;
	public float Speed;
	public float RotationSpeed;

	public UnityEvent OnCatchTarget;

	bool Dead;
	Animator animator;

	protected override void Start () 
    {
		base.Start ();

		if(!Target)
			Target = GameController.instance.PlayerPosition;

		GameController.instance.cockroaches.Add(this);

		Speed = GameController.instance.EnemiesSpeed;

		animator = GetComponent<Animator>();

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

	public void Die()
	{
		Dead = true;

        animator.SetBool("Dead",true);

        GameController.instance.cockroaches.Remove(this);
		GameController.instance.OnEnemyDie.Invoke();
		GetComponent<CircleCollider2D>().enabled = false;
        //Destroy(gameObject);
    }

    public static float CrossRotation(Vector2 Target, Transform GameObject)
    {
        Vector2 direction = Target - (Vector2)GameObject.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, GameObject.up).z;

        return -rotateAmount;
    }

    void ChaseTarget(Transform target)
	{
		float Distance = Vector2.Distance(transform.position, target.position);

		if (Distance <= 0.1f)
		{
			GameController.instance.GameOver();
			OnCatchTarget.Invoke();
			return;
		}

        //Vector2 dir = target.position - transform.position;
        //transform.right = dir.normalized;
        transform.Rotate(0, 0, (-CrossRotation(target.position, transform) * (RotationSpeed * 2.5f)));


        transform.Translate(new Vector2(0, -Speed) * Time.deltaTime);
    }
}

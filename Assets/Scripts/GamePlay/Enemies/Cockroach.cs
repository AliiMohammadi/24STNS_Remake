using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cockroach : MonoBehaviour 
{
	public Transform Target;
	public float Speed;

	public UnityEvent OnCatchTarget;

	void Start () 
    {
		if(!Target)
			Target = GameController.instance.PlayerPosition;

		GameController.instance.cockroaches.Add(this);

		Speed = GameController.instance.EnemiesSpeed;
    }
	void Update () 
    {
		if (Target)
            ChaseTarget(Target);

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

		Vector2 dir = target.position - transform.position;
		transform.right = dir.normalized;
		transform.Translate(new Vector2(Speed, 0) * Time.deltaTime);
    }

	public void Die()
	{
        GameController.instance.cockroaches.Remove(this);
		GameController.instance.OnEnemyDie.Invoke();

        Destroy(gameObject);
	}
}

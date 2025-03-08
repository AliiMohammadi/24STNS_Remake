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
			OnCatchTarget.Invoke();
			return;
		}

		Vector2 dir = target.position - transform.position;
		transform.right = dir.normalized;
		transform.Translate(new Vector2(Speed, 0) * Time.deltaTime);
    }

	public void Die()
	{
		transform.position = Vector2.zero;
		gameObject.SetActive(false);
	}
}

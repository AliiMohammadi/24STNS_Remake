using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{
	public float Speed;

	public UnityEvent OnMoveing;

	void Start () 
    {
		
	}
	void Update () 
    {
		if (Input.GetKey(KeyCode.W))
			Move();
    }

	void FixedUpdate()
	{
        if (Input.GetKey(KeyCode.W))
            OnMoveing.Invoke();
    }

    void Move()
	{
		transform.Translate(new Vector2(0,Speed) * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class TemproryObject : MonoBehaviour 
{
	protected SpriteRenderer rend;

    bool Showed;

    protected virtual void Start () 
    {
        rend = GetComponent<SpriteRenderer>();

    }
	protected virtual void Update()
	{
        if (rend.isVisible)
            Showed = true;

        CheckVisible();
    }

	protected virtual void CheckVisible()
	{
        if (Showed && !rend.isVisible || GameController.instance.IsLeftBehinded(transform.position))
            Destroy(gameObject);
    }
}

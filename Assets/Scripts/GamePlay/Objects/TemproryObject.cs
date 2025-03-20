using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class TemproryObject : MonoBehaviour 
{
	protected SpriteRenderer renderer;

    bool Showed;

    protected virtual void Start () 
    {
        renderer = GetComponent<SpriteRenderer>();

    }
	protected virtual void Update()
	{
        if (renderer.isVisible)
            Showed = true;

        CheckVisible();
    }

	protected virtual void CheckVisible()
	{
        if (Showed && !renderer.isVisible)
            Destroy(gameObject);
    }
}

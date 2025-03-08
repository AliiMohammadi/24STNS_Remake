using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfinityBackground : MonoBehaviour 
{
	public List<Transform> Backgrounds = new List<Transform>();
	public float Ydistance;

    public UnityEvent OnNextStep;

	Vector2 CameraPosition;
	int backgroundindex;

    float nextSteploc;

	public void UpdateLocation()
	{
        CameraPosition = Camera.main.transform.position;

        if (CameraPosition.y >= nextSteploc)
            SetNextBackground();
    }
	public void SetNextBackground()
	{

        ulong n = (ulong)Mathf.Ceil(CameraPosition.y / Ydistance);

        backgroundindex++;

        if (backgroundindex >= Backgrounds.Count)
            backgroundindex = 0;

        Vector2 cur = Backgrounds[backgroundindex].position;

        Backgrounds[backgroundindex].position = new Vector2(cur.x, (n * Ydistance));

        nextSteploc = n*Ydistance;

        OnNextStep.Invoke();
    }
}

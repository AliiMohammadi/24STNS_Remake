using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour 
{
	public Text ScoreText;
	ClassicMode mode;

	void Start()
	{
        mode = FindObjectOfType<ClassicMode>();	

    }
	void Update () 
    {
		ScoreText.text = mode.Score.ToString();
	}
}

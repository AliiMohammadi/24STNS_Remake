using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIpanelController : MonoBehaviour 
{
	public GameObject GameOverPanel;

	public void ShowGameOverPanel()
	{
		GameOverPanel.SetActive(true);
	}
}

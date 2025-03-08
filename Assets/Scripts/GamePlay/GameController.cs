using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{

	public static GameController instance;

	public WeaopnController PlayerWeapon;

	public GUIpanelController PanelController;

	public List<Cockroach> cockroaches = new List<Cockroach>();

	void Awake()
	{
		PanelController = FindObjectOfType<GUIpanelController>();
        PlayerWeapon = FindObjectOfType<WeaopnController>();
        cockroaches = FindObjectsOfType<Cockroach>().ToList();
    }
	void Start () 
    {
        instance = this;
    }
	void Update () 
    {
		
	}

	public void GameOver()
	{
        PanelController.ShowGameOverPanel();

        Time.timeScale = 0;
    }
}

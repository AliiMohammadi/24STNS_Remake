using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{

	public static GameController instance;

	public Transform PlayerPosition;
	public WeaopnController PlayerWeapon;

	public GUIpanelController PanelController;

	public List<Cockroach> cockroaches = new List<Cockroach>();

	public float EnemiesSpeed;

	public UnityEvent OnEnemyDie;
	public UnityEvent OnGameOver;

	void Awake()
	{
		PanelController = FindObjectOfType<GUIpanelController>();
        PlayerWeapon = FindObjectOfType<WeaopnController>();
        cockroaches = FindObjectsOfType<Cockroach>().ToList();
    }
	void Start () 
    {
        instance = this;
        Time.timeScale = 1;

    }

	public void GameOver()
	{
        PanelController.ShowGameOverPanel();

        Time.timeScale = 0;
        PlayerWeapon.enabled = false;
        OnGameOver.Invoke();
    }

	public bool IsLeftBehinded(Vector2 ObjectPosition)
	{
		return PlayerPosition.position.y > ObjectPosition.y + 10;
    }
}

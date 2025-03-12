using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficalityController : MonoBehaviour 
{
    ClassicMode game;
    ObjectSpawner spawner;

    void Start()
    {
        game = FindObjectOfType<ClassicMode>();
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    public void UpdateDifficality()
    {
        if (game.Score % 10 == 0)
            AddOneLevelHigher();
    }

    void AddOneLevelHigher()
    {
        if (spawner.Objects[0].SpawnProbeblity < 100)
            spawner.Objects[0].SpawnProbeblity += 1;

        GameController.instance.EnemiesSpeed *= 1.01f;
    }
}

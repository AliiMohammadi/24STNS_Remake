using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficalityController : MonoBehaviour 
{
    public int NextLevelCycle;

    ClassicMode game;
    ObjectSpawner spawner;

    void Start()
    {
        game = FindObjectOfType<ClassicMode>();
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    public void UpdateDifficality()
    {
        if ((int)game.Score % NextLevelCycle == 0)
            AddOneLevelHigher();
    }

    public void AddOneLevelHigher()
    {
        if (spawner.Objects[0].SpawnProbeblity < 100)
            spawner.Objects[0].SpawnProbeblity += 1f;

        GameController.instance.EnemiesSpeed *= 1.01f;

        if (spawner.Objects[1].SpawnProbeblity < 100)
            spawner.Objects[1].SpawnProbeblity += 0.025f;
    }
}

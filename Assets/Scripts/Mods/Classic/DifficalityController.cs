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
        if ((int)game.Score % NextLevelCycle != 0)
            return;

        AddOneLevelHigher();

        if (400 < (int)game.Score)
            NextLevelCycle++;
    }

    public void AddOneLevelHigher()
    {
        Add(0, 1);
        Add(1, 0.025f);
        Add(5, 0.001f);

        GameController.instance.EnemiesSpeed *= 1.01f;
    }
    
    void Add(int index,float value)
    {
        if (spawner.Objects[index].SpawnProbeblity < 100)
            spawner.Objects[index].SpawnProbeblity += value;
    }
}

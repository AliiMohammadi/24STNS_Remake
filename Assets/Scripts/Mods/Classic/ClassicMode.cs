using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ClassicMode : MonoBehaviour 
{
    public ulong Score;

    public float StepSpawn;

    public AudioSource GameplayMusic;

    public Material DarknessMaterial;

    public Light PlayerLight;

    public GameObject Rain1;
    public GameObject Rain2;
    public GameObject Rain3;
    public GameObject RainSound;

    Clock clock;

    ObjectSpawner spawner;

    float StartYstep;
    ulong steps;

    bool SpawnedUzi;

    int RandomRainstart;

    void Start()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
        StartYstep = GameController.instance.PlayerPosition.position.y;
        clock = FindObjectOfType<Clock>();

        DarknessMaterial.color = new Color(1,1,1);
        clock.SetTime(new System.TimeSpan(7,30,0));

        RandomRainstart = Random.Range(120,160);
    }

    public void AddScore()
    {
        Score++;
    }

    public void UpdateStepCounter()
    {

        float playerYstep = (GameController.instance.PlayerPosition.position.y - StartYstep);

        if ((int)(playerYstep % StepSpawn) == 0)
            spawner.SpawnRandom();

        float Co = (255 - playerYstep) / 255;
        float light = (1 /(( (255 - playerYstep)) / 35)) * 60;

        if(playerYstep < 220)
            DarknessMaterial.color = new Color(Co, Co, Co);

        if (light > 10)
            PlayerLight.range = light;

        if (!SpawnedUzi && Score > 100)
        {
            SpawnedUzi = true;
            spawner.SpawnItem(spawner.Objects[2]);
        }

        if(!GameplayMusic.isPlaying && Score > 15)
            GameplayMusic.Play();


        if(Score > (ulong)RandomRainstart)
        {
            if (!Rain1.activeSelf)
            {
                Rain1.SetActive(true);
                RainSound.SetActive(true);
            }
            else if (Score > (ulong)RandomRainstart + 20)
            {
                Rain2.SetActive(true);
            }
            else if (Score > (ulong)RandomRainstart + 40)
            {
                Rain3.SetActive(true);
            }
        }


        //clock.SetTime(new System.TimeSpan());
    }
}

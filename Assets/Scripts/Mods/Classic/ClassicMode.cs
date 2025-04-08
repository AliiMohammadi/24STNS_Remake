using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassicMode : MonoBehaviour 
{
    public ulong Score;

    public float StepSpawn;
    public float DarknesScale;

    public AudioSource GameplayMusic;

    public Material DarknessMaterial;

    public Light PlayerLight;

    public GameObject Rain1;
    public GameObject Rain2;
    public GameObject Rain3;
    public GameObject RainSound;
    public GameObject Lightning;

    public GameObject Glock45Reward;
    public GameObject Colt1911Reward;
    public GameObject UZIReward;

    public GameObject GamePlayUI;
    public GameObject MenuUI;

    public int MusicAt;

    public int SpawnUZIAt;

    public int StartRainAt;

    float Steps
    {
        get
        {
            return (GameController.instance.PlayerPosition.position.y - StartYstep);
        }
    }

    ObjectSpawner spawner;

    float StartYstep;
    bool Gamestarted;
    bool SpawnedUzi;

    bool Rained;

    int RandomRainstart;
    int NextLighning;

    uint DefualtMagAmount;

    void Start()
    {

        Screen.SetResolution(1439,1080,true);
        Application.targetFrameRate = 20;

        spawner = FindObjectOfType<ObjectSpawner>();
        StartYstep = GameController.instance.PlayerPosition.position.y;
        //clock = FindObjectOfType<Clock>();

        DarknessMaterial.color = new Color(1,1,1);
        //clock.SetTime(new System.TimeSpan(7,30,0));

        RandomRainstart = (int)Random.Range(StartRainAt, StartRainAt * 1.33f);

        NextLighning = (int)Random.Range(RandomRainstart, RandomRainstart * 2.5f);

        DefualtMagAmount = GameController.instance.PlayerWeapon.Mags;
        GameController.instance.PlayerWeapon.Mags = uint.MaxValue;

        RewardWeaponsCheck();

    }

    void Update()
    {
        Application.targetFrameRate = 20;

        if (!Input.GetKeyDown(KeyCode.W))
            return;
        if (Gamestarted)
            return;

        Gamestarted = true;

        HideMenu();
        RestartWeapon();
    }
     
    public void AddScore()
    {
        Score++;
    }
    public void UpdateStepCounter()
    {
        RandomSpawn();
        NightDencitySet();
        MusicSet();
        ChechWeaponStates();
        SetRain();
        SparkLightning();

        if (750 < Score)
            ReleasTeenCokcroaches();
    }

    void RewardWeaponsCheck()
    {
        int record = SaveAndLoad.TopRecord;

        if(record >= 1000)
            Glock45Reward.SetActive(true);
        if (record >= 1700)
            Colt1911Reward.SetActive(true);
        if (record >= 2500)
            UZIReward.SetActive(true);
    }
    void HideMenu()
    {
        MenuUI.SetActive(false);
        GamePlayUI.SetActive(true);
    }
    void RestartWeapon()
    {
        GameController.instance.PlayerWeapon.Ammo = GameController.instance.PlayerWeapon.Weapons[0].MagCapacity;
        GameController.instance.PlayerWeapon.Mags = DefualtMagAmount;
    }

    void RandomSpawn()
    {
        if ((int)(Steps % StepSpawn) == 0)
            spawner.SpawnRandom();
    }
    void NightDencitySet()
    {
        float distance = Steps / DarknesScale;

        if (distance > 220)
            return;


        float Co = (255 - distance) / 255;
        float light = (1 / (((255 - distance)) / 35)) * 60;


        DarknessMaterial.color = new Color(Co, Co, Co);

        if (light > 10)
            PlayerLight.range = light;
    }
    void MusicSet()
    {
        if (!GameplayMusic.isPlaying && (int)Score > MusicAt)
            GameplayMusic.Play();
    }
    void ChechWeaponStates()
    {
        if (!SpawnedUzi && Score > (ulong)SpawnUZIAt)
        {
            SpawnedUzi = true;
            spawner.SpawnItem(spawner.Objects[2]);
        }
    }
    void SetRain()
    {

        if (Score > (ulong)RandomRainstart)
        {
            Rained = true;

            if (!Rain1.activeSelf)
            {
                Rain1.SetActive(true);
                RainSound.SetActive(true);
            }
            else if (Score > (ulong)RandomRainstart + 30)
            {
                Rain2.SetActive(true);
            }
            else if (Score > (ulong)RandomRainstart + 60)
            {
                Rain3.SetActive(true);
            }
        }
    }
    void SparkLightning()
    {
        if (!Rained)
            return;

        if ((long)Score < NextLighning)
             return;

        Lightning.SetActive(false);
        Lightning.SetActive(true);

        NextLighning = (int)Random.Range(Score, Score + 100);
    }
    void ReleasTeenCokcroaches()
    {
        spawner.Objects[12].SpawnProbeblity = 1.5f;
    }

    public void GameOver()
    {
        GameplayMusic.Stop();

        if(SaveAndLoad.TopRecord < (int)Score)
            SaveAndLoad.TopRecord = (int)Score;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

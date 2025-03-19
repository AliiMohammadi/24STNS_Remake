using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public enum SpawnMode { InView, OutView }
    [SerializeField]
    public List<ObjectInfo> Objects = new List<ObjectInfo>();

    public SpawnMode Spawnmode;

    float Camwidth;
    float Camheight;

    void Start()
    {
        Camwidth = Camera.main.pixelWidth / 200;
        Camheight = Camera.main.pixelHeight / 200;
    }

    public void SpawnRandom()
    {
        switch (Spawnmode)
        {
            case SpawnMode.InView:
                break;
            case SpawnMode.OutView:
                foreach (var item in Objects)
                    if (item.CanSpawn())
                        SpawnItem(item);
                break;
            default:
                break;
        }
    }
    public void SpawnSome(int n)
    {
        for (int i = 0; i < n; i++)
            SpawnRandom();
    }
    public void SpawnItem(ObjectInfo item)
    {
        Vector2 CameraPos = Camera.main.transform.position;

        Vector2 Position = new Vector2(CameraPos.x + UnityEngine.Random.Range(-Camwidth, Camwidth), CameraPos.y + UnityEngine.Random.Range(Camheight, Camheight * 4));
        Instantiate(item.gameObject, null).transform.position = Position;
    }

    [Serializable]
    public class ObjectInfo
    {
        [SerializeField]
        public GameObject gameObject;
        [Range(0, 100)]
        public float SpawnProbeblity;

        public bool CanSpawn()
        {
            return (SpawnProbeblity >= (UnityEngine.Random.Range(1,1000000)/10000f));
        }
    }
}

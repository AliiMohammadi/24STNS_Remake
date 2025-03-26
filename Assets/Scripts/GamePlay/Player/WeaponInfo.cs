using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct WeaponInfo
{
    public GameObject Appearance;

    public WeaopnController.FierMode Fiermode;
    public uint MagCapacity;
    public AudioClip FeirSound;

    public float MinPitchSound;
    public float MaxPitchSound;
}

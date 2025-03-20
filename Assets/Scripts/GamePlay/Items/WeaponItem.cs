using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : TemproryObject 
{
    public AudioClip PickSound;

    public int WeaponIndex;

    WeaopnController controler;

    protected override void Start()
    {
        base.Start();
        controler = FindObjectOfType<WeaopnController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
            return;

        controler.SetWeaponTo(controler.Weapons[WeaponIndex]);
        SoundPlayer.PlayAudio(PickSound);
        Destroy(gameObject);
    }
}

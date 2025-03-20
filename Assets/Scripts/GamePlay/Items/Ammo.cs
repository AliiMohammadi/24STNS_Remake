using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.UI;

public class Ammo : TemproryObject 
{
    public AudioClip PickSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player")
            return;

        GameController.instance.PlayerWeapon.Mags++;
        SoundPlayer.PlayAudio(PickSound);
        Destroy(gameObject);
    }
}

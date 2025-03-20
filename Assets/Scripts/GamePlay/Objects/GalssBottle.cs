using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GalssBottle : TemproryObject 
{
    public AudioClip BreackSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            return;

        SoundPlayer.PlayAudio(BreackSound,0.5f);
        Destroy(gameObject);

    }
}

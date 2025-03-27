using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BreakableObject : TemproryObject 
{
    public AudioClip BreackSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            return;

        Break();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            return;

        Break();
    }

    void Break()
    {
        SoundPlayer.PlayAudio(BreackSound, 0.5f);
        Destroy(gameObject);
    }
}

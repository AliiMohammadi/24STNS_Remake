using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveObject : TemproryObject
{

    public GameObject Explosive;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            return;

        Explode();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            return;

        Explode();

    }

    void Explode()
    {
        Instantiate(Explosive, transform).transform.SetParent(null);
        Destroy(gameObject);
    }
}

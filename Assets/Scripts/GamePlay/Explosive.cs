using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Explosive : MonoBehaviour 
{

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
            GameController.instance.cockroaches.Where(x => x.gameObject.Equals(collision.gameObject)).First().Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour 
{
	public float Speed;
	public sbyte MaxDeflect = 1;

	public AudioClip[] ReflectSounds;

	Rigidbody2D rigid;
	sbyte Hitcount;

	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * Time.deltaTime * Speed;

		Destroy(gameObject,1);
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
		Hitcount++;

        SoundPlayer.PlayAudio(ReflectSounds[Random.Range(0, ReflectSounds.Length-1)]);

		if(collision.gameObject.tag == "Enemy")
            GameController.instance.cockroaches.Where(x => x.gameObject.Equals(collision.gameObject)).First().Die();


        if (Hitcount > MaxDeflect)
			Destroy(gameObject);
    }
}

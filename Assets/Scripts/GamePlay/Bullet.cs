using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour 
{
	public enum BulletType
	{
		Ordinal,Brutal
	}
	public BulletType type;
	public float Speed;
	public float LifeTime;
	public sbyte MaxDeflect = 1;
	public bool FriendlyFier;
	public AudioClip[] ReflectSounds;

	Rigidbody2D rigid;
	sbyte Hitcount;

	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * Time.deltaTime * Speed;

		Destroy(gameObject, LifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
		if (type == BulletType.Brutal)
			return;

		Hitcount++;

        SoundPlayer.PlayAudio(ReflectSounds[Random.Range(0, ReflectSounds.Length-1)]);

		if (IsEnemy(collision.gameObject))
            GameController.instance.KillCockRoach(collision.gameObject);

        if (FriendlyFier)
            if (collision.gameObject.tag == "Player")
                GameController.instance.GameOver();

        if (Hitcount > MaxDeflect)
			Destroy(gameObject);
    }
	void OnTriggerEnter2D(Collider2D collision)
	{
        if (FriendlyFier)
            if (collision.gameObject.tag == "Player")
                GameController.instance.GameOver();

        if (type != BulletType.Brutal)
            return;

        SoundPlayer.PlayAudio(ReflectSounds[Random.Range(0, ReflectSounds.Length - 1)]);

        if (IsEnemy(collision.gameObject))
            GameController.instance.KillCockRoach(collision.gameObject);

        if (FriendlyFier)
            if (collision.gameObject.tag == "Player")
                GameController.instance.GameOver();
    }

    protected bool IsEnemy(GameObject gameObject)
	{
		return gameObject.gameObject.tag == "Enemy";
    }
}

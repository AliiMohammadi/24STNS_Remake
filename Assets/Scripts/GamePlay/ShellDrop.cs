using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class ShellDrop : TemproryObject 
{
	public float ForceMo;
	public float FramMo;
	public int frame = 110;

    public AudioClip[] ShellSounds;

	int framecounter;
	int Maxhitcount;
	int hitcount;

	Rigidbody2D rigid;

	protected override void Start()
	{
        base.Start();
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddTorque(Random.Range(4000, 8000));
        rigid.AddForce(new Vector2(-Random.Range(25,60), 0));
		Maxhitcount = Random.Range(2,4);
        frame = Random.Range(30, 80);
    }

    protected override void Update () 
    {

		if (frame == 0 || hitcount > Maxhitcount)
		{
			rigid.rotation = 0;
            rigid.gravityScale = 0;
            rigid.velocity = Vector3.zero;

            base.Update();

            return;
		}

		framecounter++;

		if(framecounter % frame != 0)
            return;

        framecounter = 0;

        rigid.AddForce(new Vector2(-Random.Range(100, 200), Random.Range(frame * 9.8f, frame * 9.8f * ForceMo)));

        frame = (int)(frame * FramMo);

        hitcount++;

        SoundPlayer.PlayAudio(ShellSounds[Random.Range(0, ShellSounds.Length)], 0.5f);
    }
}

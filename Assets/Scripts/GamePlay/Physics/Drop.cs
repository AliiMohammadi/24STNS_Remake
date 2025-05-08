using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drop : MonoBehaviour 
{
	public float Height;
	public float DeflectScale;
    public Vector2 Direction;
	public float JumpDecrementScale;

    [HideInInspector]
	public Rigidbody2D Rigid;
    
	int frame = 110;
	int framecounter;
	int Maxhitcount;
	int hitcount;

	protected virtual void Start()
	{
        Rigid = GetComponent<Rigidbody2D>();

		if(DeflectScale > 0.000f)
            Maxhitcount = (int)Random.Range(DeflectScale, 2 * DeflectScale);
       
        frame = (int)Height * 60;
    }

    protected virtual void Update () 
    {
        framecounter++;

		if(framecounter % frame != 0)
            return;

        TouchingGround();
    }

    protected virtual void TouchingGround()
    {
        if (frame == 0 || hitcount > Maxhitcount)
        {
            Stop();

            return;
        }
        
        hitcount++;

        framecounter = 0;

        frame = (int)(frame / JumpDecrementScale);

        Rigid.AddForce(new Vector2(Direction.x, frame * Direction.y * 9.8f * Height));

    }
    protected virtual void Stop()
    {
        Rigid.rotation = 0;
        Rigid.gravityScale = 0;
        Rigid.velocity = Vector3.zero;
    }
}

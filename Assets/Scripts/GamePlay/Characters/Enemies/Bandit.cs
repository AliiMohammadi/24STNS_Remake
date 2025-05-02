using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bandit : NPC 
{
    public float AimLeastDistance;
    int AimDelay = 140;

    public GameObject BulletPrifab;
    public Transform FierPosition;
    public AudioClip ShotSound;

    public List<Transform> Targets;

    protected Animator animator;

    public UnityEvent OnGetScore;

    bool shotallow;
    int x;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        Targets = new List<Transform>();

    }
    protected override void Update()
    {
        base.Update();

        Targets.Clear();

        Targets.Add(GameController.instance.PlayerPosition);

        foreach (Cockroach t in GameController.instance.cockroaches)
            Targets.Add(t.transform);

        if (Targets.Count > 0 && !Dead)
            AttackTarget(GetNearstTarget());
    }

    public override void Die()
    {
        animator.SetBool("Dead", true);

        GameController.instance.Bandits.Remove(this);
        GameController.instance.OnEnemyDie.Invoke();
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 0;

        base.Die();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            TakeDamage(50);
    }

    void AttackTarget(Transform target)
    {
        float distance = transform.position.y - target.position.y;

        if(Mathf.Abs(distance) > AimLeastDistance)
            Move(new Vector2(0 , target.position.y));
        else
        {
            if (!AimAt(target))
                return;

            if (!shotallow)
            {
                x++;

                if (x > AimDelay)
                    shotallow = true;

                return;
            }

            Shot();
            shotallow = false;
            x = 0;
        }
    }
    Transform GetNearstTarget()
    {
        float nearestDistance = 100.0000f;

        Transform nearest = Targets.First();

        foreach (var item in Targets)
        {
            float dis = Vector2.Distance(transform.position,item.position);

            if(dis < nearestDistance)
            {
                nearestDistance = dis;
                nearest = item;
            }
        }

        return nearest;
    }

    bool AimAt(Transform location)
    {
        LookAt(location.position);

        Vector2 direction = (Vector2)location.position - (Vector2)transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        return (Mathf.Abs(rotateAmount) < 0.1f);
    }
    void Shot()
    {
        Instantiate(BulletPrifab, FierPosition).transform.SetParent(null);

        SoundPlayer.PlayAudio(ShotSound, 0.6f, UnityEngine.Random.Range(0.9701f, 1.0701f));

        animator.SetTrigger("Shot");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bandit : NPC
{
    [HideInInspector]
    public int AimDelay = 140;
    [HideInInspector]
    public float AimLeastDistance;

    [SerializeField]
    public List<TargetInfo> Targets;

    public GameObject FlashLight;
    public GameObject BulletPrifab;
    public Transform FierPosition;
    public AudioClip ShotSound;


    protected Animator animator;

    AudioSource FootStep;

    int delayValue;
    bool shotallow;
    int x;
    float distancelimit = 2;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();

        Targets = new List<TargetInfo>();

        FootStep = GetComponent<AudioSource>();
        delayValue = AimDelay;
    }
    protected override void Update()
    {
        base.Update();

        Targets.Clear();

        Targets.Add(new TargetInfo(GameController.instance.PlayerPosition,1, TargetInfo.AimTypes.Accurate));

        foreach (Cockroach t in GameController.instance.cockroaches)
            Targets.Add(new TargetInfo(t.transform, 1, TargetInfo.AimTypes.Unaccurate));

        if (Targets.Count > 0 && !Dead)
            AttackTarget(GetNearstTarget());
    }

    public override void Die()
    {
        animator.SetBool("Dead", true);
        FlashLight.SetActive(false);
        FootStep.Stop();
        GetComponent<Rigidbody2D>().AddForce(transform.up * 40000);

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

    void AttackTarget(TargetInfo target)
    {
        float distance = transform.position.y - target.Object.position.y;

        if (target.AimType == TargetInfo.AimTypes.Unaccurate)
        {
            if (Mathf.Abs(distance) > distancelimit)
                LesMove(target);
            else
                FootStep.Stop();

            if (Mathf.Abs(distance) < 10)
                AimAndShot(target);
        }
        if (target.AimType == TargetInfo.AimTypes.Accurate)
        {
            if(Mathf.Abs(distance) > AimLeastDistance)
                LesMove(target);
            else
            {
                FootStep.Stop();

                AimAndShot(target);
            }
        }
    }
    TargetInfo GetNearstTarget()
    {
        float nearestDistance = 100.0000f;

        TargetInfo nearest = Targets.First();

        foreach (var item in Targets)
        {
            float dis = 0.0000000f;

            dis = Vector2.Distance(transform.position, item.Object.position) / item.Priority;

            if (dis < nearestDistance)
            {
                nearestDistance = dis;
                nearest = item;
            }
        }

        return nearest;
    }

    void LesMove(TargetInfo t)
    {
        Move(new Vector2(0, t.Object.position.y));

        if (!FootStep.isPlaying)
            FootStep.Play();
    }
    void AimAndShot(TargetInfo t)
    {
        if (!AimAt(t.Object))
            return;

        if (!shotallow)
        {
            x++;

            float distance = Vector2.Distance(transform.position,t.Object.position);

            if (distance < AimLeastDistance)
                delayValue = (int)((distance / (AimLeastDistance - distancelimit)) * AimDelay);

            if (x > Math.Abs(delayValue))
                shotallow = true;

            return;
        }

        if(shotallow)
            Shot();
        shotallow = false;
        x = 0;
        delayValue = AimDelay;
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


    [Serializable]
    public class TargetInfo
    {
        public enum AimTypes
        {
            Accurate,Unaccurate
        }
        [SerializeField]
        public Transform Object;

        public float Priority;
        public AimTypes AimType;
        
        public TargetInfo(Transform obje, float pri , AimTypes aimTypes)
        {
            Object = obje;
            Priority = pri;
            AimType = aimTypes;
        }
    }
}

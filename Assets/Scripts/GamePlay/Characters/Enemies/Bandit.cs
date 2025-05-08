using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    public bool Friendly;

    public GameObject FlashLight;
    public GameObject BulletPrifab;
    public GameObject ShellObject;
    public Transform FierPosition;
    public Transform ShellDropPosition;
    public AudioClip ShotSound;

    protected Animator animator;

    protected AudioSource FootStep;

    protected int AimRecoil = 10;
    protected float MaximumDistanceToShot = 2;
    protected float MinimumDistanceToShot = 10;

    int delayValue;
    bool shotallow;
    int x;

    Vector3 startangle;

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

        if(!Friendly)
            Targets.Add(new TargetInfo(GameController.instance.PlayerPosition,2.5f, TargetInfo.AimTypes.Accurate));

        foreach (Cockroach t in GameController.instance.cockroaches)
            Targets.Add(new TargetInfo(t.transform, 1, TargetInfo.AimTypes.Unaccurate));


        if (IsUndecided())
            IdleMode();
        else
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

        Dead = true;

        base.Die();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            TakeDamage(50);
    }
    protected virtual void IdleMode()
    {
        if(FootStep.isPlaying)
            FootStep.Stop();

    }
    protected virtual void AttackTarget(TargetInfo target)
    {
        if (Dead)
            return;

        float distance = transform.position.y - target.Object.position.y;

        if (target.AimType == TargetInfo.AimTypes.Unaccurate)
        {

            if (Mathf.Abs(distance) < MinimumDistanceToShot)
                AimAndShot(target);

            if (Mathf.Abs(distance) > MaximumDistanceToShot)
                LesMove(target);
            else
                FootStep.Stop();
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
    protected TargetInfo GetNearstTarget()
    {
        if (Dead)
            return null;

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

    protected void LesMove(TargetInfo t)
    {
        Move(t.Object.position);

        if (!FootStep.isPlaying)
            FootStep.Play();
    }

    protected void AimAndShot(TargetInfo t)
    {
        if (!AimAt(t.Object))
            return;

        if (!shotallow)
        {
            x++;

            float distance = Vector2.Distance(transform.position,t.Object.position);

            if (distance < AimLeastDistance)
                delayValue = (int)((distance / (AimLeastDistance - MaximumDistanceToShot)) * AimDelay);

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

    protected bool AimAt(Transform location)
    {
        LookAt(location.position);

        Debug.DrawLine(transform.position, location.position, Color.red);

        Vector2 direction = (Vector2)location.position - (Vector2)transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        return (Mathf.Abs(rotateAmount) < 0.1f);
    }
    protected virtual void Shot()
    {
        startangle = FierPosition.eulerAngles;

        FierPosition.eulerAngles = new Vector3(
        FierPosition.eulerAngles.x,
        FierPosition.eulerAngles.y ,
        FierPosition.eulerAngles.z + UnityEngine.Random.Range(-AimRecoil, AimRecoil)
        );

        Instantiate(BulletPrifab, FierPosition).transform.SetParent(null);
        Instantiate(ShellObject, ShellDropPosition).transform.SetParent(null);
        FierPosition.eulerAngles = startangle;
        SoundPlayer.PlayAudio(ShotSound, 0.6f, UnityEngine.Random.Range(0.9701f, 1.0701f));

        animator.SetTrigger("Shot");
    }

    protected bool IsUndecided()
    {
        return (Targets.Count == 0 && !Dead);
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

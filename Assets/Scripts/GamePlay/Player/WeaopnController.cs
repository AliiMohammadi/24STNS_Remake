using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaopnController : MonoBehaviour 
{
    public enum FierMode
    {
        None, Brust, Auto
    }
    [SerializeField]
    public List<WeaponInfo> Weapons = new List<WeaponInfo>(); 

    public FierMode fierMode = FierMode.None;

    public GameObject WeaponObject;
	public GameObject BulletPrifab;
	public GameObject ShellPrifab;

	public Transform RayObject;
	public Transform RayPosittion;
	public Transform ShellDropPosition;
	public Transform RayPoint;
	public Transform ProjectilePosition;

	public Animator Animator;

    public uint Mags;
    public uint Ammo;

    public AudioClip ShotSound;
    public AudioClip ReloadSound;
    public AudioClip EmptySound;

    public UnityEvent OnShot;
    public UnityEvent OnReload;

	bool IsAiming;
    uint MagCap = 12;

    float MinPitchSound;
    float MaxPitchSound;

    void Start()
    {
        MinPitchSound = 0.9500f;
        MinPitchSound = 1.0700f;
    }

	void Update () 
    {
		IsAiming = Input.GetMouseButton(1);

        CheckAiming();

        CheckShot(ThrowRaycast());
    }

    void CheckAiming()
    {
        if (!IsAiming)
        {
            WeaponObject.transform.up = Vector2.zero;

            WeaponObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, WeaponObject.transform.position.y);
        }
        else
        {
            WeaponObject.transform.up = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)WeaponObject.transform.position).normalized;
        }
    }
    RaycastHit2D ThrowRaycast()
    {
        RaycastHit2D RayHit = Physics2D.Raycast(RayPosittion.position, RayPosittion.up);

        if (!RayHit)
        {
            //Red point stuff
            RayPoint.gameObject.SetActive(false);
            RayObject.localScale = new Vector2(RayPosittion.localScale.x, 150);
            return RayHit;
        }
        
        float distance = Vector2.Distance(RayHit.point, RayPosittion.position);

        RayObject.localScale = new Vector2(RayObject.localScale.x, distance * 10);

        RayPoint.gameObject.SetActive(true);
        RayPoint.position = RayHit.point;

        return RayHit;
    }
    GameObject CheckShot(RaycastHit2D hit)
    {
        if (fierMode == FierMode.Auto)
            if (!Input.GetMouseButton(0))
            {
                Animator.SetBool("ShotLock", false);

                return null;
            }


        if (fierMode == FierMode.None)
            if (!Input.GetMouseButtonDown(0))
                return null;

        if (Ammo <= 0)
            if(Mags <= 0)
            {
                AudioSource.PlayClipAtPoint(EmptySound, transform.position);
                return null;
            }
            else
            {
                Reload();
                return null;

            }

        Shot();

        if (!hit)
            return null;

        GameObject target = hit.transform.gameObject;

        return target;

    }

    void Shot()
    {
        if (fierMode == FierMode.None)
            Animator.SetTrigger("Shot");
        if (fierMode == FierMode.Auto)
            Animator.SetBool("ShotLock", true);

    }
    void Reload()
    {
        Mags--;
        Ammo = MagCap;
        SoundPlayer.PlayAudio(ReloadSound);
        Animator.SetTrigger("Reload");
        OnReload.Invoke();
    }

    public void Fier()
    {
        Ammo--;

        Instantiate(BulletPrifab, ProjectilePosition).transform.SetParent(null);
        Instantiate(ShellPrifab, ShellDropPosition).transform.SetParent(null);


        SoundPlayer.PlayAudio(ShotSound,0.6f,UnityEngine.Random.Range(MinPitchSound*100, MinPitchSound*100)/100);
        //SoundPlayer.PlayAudio(ShotSound,1,1);

        OnShot.Invoke();

    }
    public void SetWeaponTo(WeaponInfo weapon)
    {
        foreach (var item in Weapons)
            item.Appearance.SetActive(false);

        weapon.Appearance.SetActive(true);

        ShotSound = weapon.FeirSound;
        MagCap = weapon.MagCapacity;
        fierMode = weapon.Fiermode;
        MinPitchSound = weapon.MinPitchSound;
        MaxPitchSound = weapon.MaxPitchSound;
        BulletPrifab = weapon.Bullet;

    }
}

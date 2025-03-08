using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaopnController : MonoBehaviour 
{
	public GameObject WeaponObject;
	public GameObject BulletPrifab;

	public Transform RayObject;
	public Transform RayPosittion;
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

        //if (target.tag != "Enemy")
        //    return target;

        //GameController.instance.cockroaches.Where(x => x.gameObject.Equals(target)).First().Die();

        return target;

    }

    void Shot()
    {
        Ammo--;
        Instantiate(BulletPrifab, ProjectilePosition).transform.SetParent(null);
        AudioSource.PlayClipAtPoint(ShotSound,transform.position);
        Animator.SetTrigger("Shot");
        OnShot.Invoke();
    }
    void Reload()
    {
        Mags--;
        Ammo = 12;
        AudioSource.PlayClipAtPoint(ReloadSound, transform.position);
        Animator.SetTrigger("Reload");
        OnReload.Invoke();
    }
}

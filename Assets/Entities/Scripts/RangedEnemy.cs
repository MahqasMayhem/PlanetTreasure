﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    #region Variables
    public float moveRadius,viewRadius, speed;
    public float enemyFlipScale;

    public Transform origin;

    private int entLayerMask;
    private Vector3 target;
    private GameObject Weapon, BeamOrigin, BeamTargeting, BeamFiring;
    private float spriteScaleX, range;
    private Animator anim;
    private AudioSource sound;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        Weapon = gameObject.transform.Find("Weapon").gameObject;
        BeamOrigin = Weapon.transform.Find("BeamOrigin").gameObject;
        BeamTargeting = Weapon.transform.Find("BeamTargeting").gameObject;
        BeamFiring = Weapon.transform.Find("BeamFiring").gameObject;
        enemyFlipScale = this.transform.localScale.x;

        entLayerMask = 1 << 8;
        origin = this.transform;

    }
    /*
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
    */
    #region Control Functions

    public void Target()
    {
        if (!anim.GetBool("isFiring"))
        {
            //anim.SetBool("isFiring", true);
            //anim.SetTrigger("setFiring");
            Debug.Log("Targeting laser!", this);

            BeamTargeting.SetActive(true);
            //StartCoroutine(Attack());
            //anim.SetTrigger("setFiring");
            //anim.SetBool("isFiring", true);
            Invoke("Fire", 2f);
        }
    }

    public void Fire()
    {
        Debug.Log("Firing laser!", this);
        BeamTargeting.SetActive(false);
        BeamFiring.SetActive(true);
        sound.Play();
        RaycastHit2D hit = Physics2D.Raycast(BeamOrigin.transform.position, Vector2.right, 30, entLayerMask);
        if (hit.collider != null)
        {
            OnHitObject(hit);
        }
        else
        {
            Debug.Log("Did not hit", this);
        }
        Invoke("ExitFiringState", 1f);
    }
    private void ExitFiringState()
    {
        BeamFiring.SetActive(false);
        anim.SetBool("isFiring", false);
        anim.SetTrigger("firingComplete");
    }
    IEnumerator Attack()
    {
        anim.SetBool("isFiring", true);
        anim.SetTrigger("setFiring");
        Debug.Log("Firing laser!", this);

        BeamTargeting.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        BeamTargeting.SetActive(false);
        BeamFiring.SetActive(true);
        RaycastHit2D hit = Physics2D.Raycast(BeamOrigin.transform.position, Vector2.right, 30, entLayerMask);
        if (hit.collider != null)
        {
            OnHitObject(hit);
        }
        else
        {
            Debug.Log("Did not hit", this);
        }
        yield return new WaitForSeconds(1f);

        BeamFiring.SetActive(false);
        anim.GetBehaviour<RangedAim>().timerFire = false;
        anim.SetBool("isFiring", false);
        anim.SetBool("isAiming", false);
    }
    private void OnHitObject(RaycastHit2D hit)
    {
        Debug.Log("Laser hit:" + hit.transform.gameObject);
        hit.transform.gameObject.BroadcastMessage("OnDamage");
    }
    #endregion
}

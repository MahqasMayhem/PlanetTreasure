using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    #region Variables
    public float moveRadius,viewRadius, speed;
    public Vector3 origin;

    private int direction;
    private Transform Weapon;
    private Rigidbody2D rb;
    private Vector3 target;
    private GameObject player;
    private float spriteScaleX, range;
    private bool lockAim, isAiming;
    private Vector3 rayStart;
    private Animator anim;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        Weapon = transform.Find("Weapon");
        rb = GetComponent<Rigidbody2D>();
        //spriteScaleX = this.transform.localScale.x;
        player = GameObject.Find("Player");
        anim = gameObject.GetComponent<Animator>();

        lockAim = false;
        isAiming = false;
        origin = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    #region Control Functions

    public void Fire()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        anim.SetBool("isFiring", true);
        Debug.Log("Firing laster!", this);

        rayStart = transform.Find("rayOrigin").position;


        yield return new WaitForSeconds(4f);
        if (Physics.Raycast(rayStart, Weapon.transform.right, out RaycastHit hit, Mathf.Infinity))
        {
            OnHitObject(hit);


        }
        anim.SetBool("isFiring", false);
        anim.SetBool("isAiming", false);
    }
    private float CalcRange(GameObject entity, float range)
    {
        range = Vector2.Distance(entity.transform.position, this.transform.position);
        //Debug.Log(range);
        return range;
    }

    private void OnHitObject(RaycastHit hit)
    {
        Debug.Log("Laser hit:" + hit.transform.gameObject);
        if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Entity"))
        {
            hit.transform.gameObject.BroadcastMessage("OnDamage");
        }
    }
    #endregion
}

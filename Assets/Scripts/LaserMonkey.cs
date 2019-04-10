using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMonkey : MonoBehaviour
{
    #region Variables
    public float moveRadius;
    public float viewRadius;
    public float speed;

    private int direction;
    private Transform Weapon;
    private Rigidbody2D rb;
    private Vector3 target;
    private GameObject player;
    private float spriteScaleX, range;
    private bool lockAim, isAiming;
    private Vector3 origin;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        Weapon = transform.Find("Weapon");
        rb = GetComponent<Rigidbody2D>();
        //spriteScaleX = this.transform.localScale.x;
        player = GameObject.Find("Player");
        lockAim = false;
        isAiming = false;
        origin = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Weapon.GetComponent<SpriteRenderer>().isVisible == true && lockAim != true)
        {
            if (viewRadius == 0)
            {
                TargetEntity(player);
            }
            else if (viewRadius > 0)
            {
                TargetEntity(viewRadius);
            }
            else
            {
                Debug.LogError("Error: Value out of Range", this);
            }
        }

    }

    void FixedUpdate()
    {
        if (isAiming == false)
        {
            Move();
            Debug.Log("Trigger Move");
        }
        else if (isAiming == true)
        {
            isAiming = !isAiming;
        }
    }

    #region Control Functions

    private void Move()
    {
        Debug.Log("move.");
        if (moveRadius > this.transform.position.x * direction - origin.x)
            {
                rb.velocity = new Vector2(speed * direction, rb.velocity.y);
            }
        else
            {
            ChangeDirection(direction * -1);
            
            }

        }
        

    
    private void ChangeDirection(int facing)
    {
        direction = facing;
        if (facing == 1)
        {
            this.transform.localScale = new Vector3(1.771533f, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(-1.771533f, this.transform.localScale.y, this.transform.localScale.z);
        }
        
    }


    private void TargetEntity(GameObject entity) //Will only be used if viewRadius is 0
    {

        isAiming = true;
        Weapon.transform.right = entity.transform.position - Weapon.transform.position;
            if (entity.transform.position.x < this.transform.position.x)
            {
                ChangeDirection(-1);
            }
            else
            {
                ChangeDirection(1);
            }
        
        
    }

    private void TargetEntity(float radius) //Will be used if viewRadius is not 0
    {

        if (this.GetComponent<SpriteRenderer>().isVisible == false && isAiming == true)
        {
            isAiming = false;
            
        }
       else if (CalcRange(player, range) < viewRadius)
        {
            TargetEntity(player);
        }
    }
    

    private float CalcRange(GameObject entity, float range)
    {
        range = Vector2.Distance(entity.transform.position, this.transform.position);
        //Debug.Log(range);
        return range;
    }

    private void FireBeam() //TODO: Begin fire sequence. Flash beam, countdown to fire, then fire. Raycast to calculate hit. Reset lockAim and isFiring
    {
        lockAim = true;
        Debug.Log("Firing laser!", this);

    }
    #endregion
}

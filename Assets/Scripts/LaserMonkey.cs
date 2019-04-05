using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMonkey : MonoBehaviour
{
    #region Variables
    public float moveRadius;
    public float speed;

    private int direction;
    private bool visible;
    private Transform Weapon;
    private Rigidbody2D rb;
    private Vector3 target;
    private GameObject player;


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        visible = false;
        Weapon = transform.Find("Weapon");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TargetEntity(player);
    }

    #region Control Functions
    private void Move()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

    }
    private void ChangeDirection() //TODO# flip the sprite and weapon together. 
    {
        direction = direction * -1;
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
    }


    private void TargetEntity(GameObject entity)
    {
        if (Weapon.GetComponent<SpriteRenderer>().isVisible == true)
        {
        Weapon.transform.right = entity.transform.position - Weapon.transform.position;
            if (Weapon.transform.rotation.eulerAngles.z < -90 || Weapon.transform.rotation.eulerAngles.z > 90)
            {
                ChangeDirection();
            }
        }
        

    }

    private void FireBeam()
    {

    }
    #endregion
}

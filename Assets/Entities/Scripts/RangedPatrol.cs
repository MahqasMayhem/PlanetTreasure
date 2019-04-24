using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPatrol : StateMachineBehaviour
{
    private int direction;
    private Transform origin;
    private float moveRadius, speed, viewRadius, xScale;
    private Rigidbody2D rb;
    private GameObject go,player;
    private Transform Weapon;
    private bool  isFiring, isAiming;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        go = animator.gameObject;
        direction = 1;
        player = GameObject.Find("Player");
        Weapon = go.transform.Find("Weapon");
        origin = go.GetComponent<RangedEnemy>().origin;
        moveRadius = go.GetComponent<RangedEnemy>().moveRadius;
        viewRadius = go.gameObject.GetComponent<RangedEnemy>().viewRadius;
        speed = go.GetComponent<RangedEnemy>().speed;
        rb = go.GetComponent<Rigidbody2D>();
        xScale = go.transform.localScale.x;

        ChangeDirection(1);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isFiring = animator.GetBool("isFiring");

        if (go.GetComponent<SpriteRenderer>().isVisible == true && isFiring == false)
           {
            
            if (viewRadius == 0)
            {
                animator.SetBool("isAiming", true);
            }
            else if (CalcRange(player) < viewRadius)
            {
                animator.SetBool("isAiming", true);
            }
            else
            {
                Debug.LogError("Error: Value out of Range", this);
            }
        }
        isAiming = animator.GetBool("isAiming");
        if (!isAiming)
        {
            Move();
        }
    }

    private void Move()
    {
        if (moveRadius > go.transform.position.x * direction - origin.position.x)
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
            go.transform.localScale = new Vector3(1, go.transform.localScale.y, go.transform.localScale.z);
        }
        else
        {
            go.transform.localScale = new Vector3(-1, go.transform.localScale.y, go.transform.localScale.z);
        }

    }

    private float CalcRange(GameObject entity)
    {
        float range = Vector2.Distance(entity.transform.position, go.transform.position);
        //Debug.Log(range);
        return range;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAim : StateMachineBehaviour
{
    public bool timerFire;

    private float viewRadius,xScale;
    private int direction;
    private GameObject go,player;
    private Transform Weapon,Head;
    private bool isAiming, isFiring;
    private RangedEnemy enemyScript;
    private Rigidbody2D rb;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        go = animator.gameObject;
        player = GameObject.Find("Player");
        viewRadius = go.GetComponent<RangedEnemy>().viewRadius;
        Weapon = animator.transform.Find("Weapon");
        Head = animator.transform.Find("Head");
        //xScale = enemyScript.enemyFlipScale;
        isAiming = animator.GetBool("isAiming");
        isFiring = animator.GetBool("isFiring");
        timerFire = false;
        enemyScript = go.gameObject.GetComponent<RangedEnemy>();
        rb = go.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("isFiring", isFiring);

        if (viewRadius == 0)
        {
            if (CalcRange(player) > viewRadius && isFiring == false)
            {
                isAiming = false;

            }
            else TargetEntity(viewRadius);

        }
        else if (go.GetComponent<SpriteRenderer>().isVisible == false && isFiring == false)
        {
            isAiming = false;

        }
        else if (!isFiring) TargetEntity(player);
    
    }

    private void ResetWeapon()
    {
        if (direction == -1)
        {
            Weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Weapon.transform.rotation = Quaternion.Euler(0, 0, 180);
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


        private void TargetEntity(GameObject entity) //Will only be used if viewRadius is 0
        {

        /*     
             Weapon.transform.right = entity.transform.position - Weapon.transform.position;
         Head.transform.right = entity.transform.position - Head.transform.position;
         */

        #region Rotate Weapon


            Vector2 offset = new Vector2(entity.transform.position.x - Weapon.transform.position.x, entity.transform.position.y - Weapon.transform.position.y);

            if (direction == 1 && !isFiring)
            {
                float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                Weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                Head.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else if (!isFiring)
            {
                float angle = Mathf.Atan2(offset.y * -1, offset.x * -1) * Mathf.Rad2Deg;
                Weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                Head.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

        #endregion

        if (!timerFire)
        {
            timerFire = true;
            Debug.Log("Preparing to fire!", go);
            enemyScript.Invoke("Fire", 2.5f);
        }
            if (entity.transform.position.x < go.transform.position.x)
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

            if (go.GetComponent<SpriteRenderer>().isVisible == false && !isFiring)
            {
                isAiming = false;
            }
            else if (CalcRange(go) < viewRadius)
            {
                TargetEntity(go);
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

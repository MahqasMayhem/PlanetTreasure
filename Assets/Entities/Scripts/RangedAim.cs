using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAim : StateMachineBehaviour
{
    private float viewRadius,xScale;
    private int direction;
    private GameObject go,player;
    private Transform Weapon;
    private bool isAiming, isFiring;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        go = animator.gameObject;
        player = GameObject.Find("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    private void ChangeDirection(int facing)
    {
        direction = facing;
        if (facing == 1)
        {
            go.transform.localScale = new Vector3(xScale, go.transform.localScale.y, go.transform.localScale.z);
        }
        else
        {
            go.transform.localScale = new Vector3(xScale * -1, go.transform.localScale.y, go.transform.localScale.z);
        }

    }


        private void TargetEntity(GameObject entity) //Will only be used if viewRadius is 0
        {

            isAiming = true;
            Weapon.transform.right = entity.transform.position - Weapon.transform.position;
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

            if (go.GetComponent<SpriteRenderer>().isVisible == false && isAiming == true)
            {
                isAiming = false;

            }
            else if (CalcRange(player, viewRadius) < viewRadius)
            {
                TargetEntity(player);
            }
        }

    private float CalcRange(GameObject entity, float range)
    {
        range = Vector2.Distance(entity.transform.position, go.transform.position);
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

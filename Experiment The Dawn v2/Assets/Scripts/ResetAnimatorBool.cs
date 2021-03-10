using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{

    public string targetBool0;
    public string targetBool1;
    public string targetBool2;
    public string targetBool3;
    public string targetBool4;
    public bool status0;
    public bool status1;
    public bool status2;
    public bool status3;
    public bool status4;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool0, status0);
        animator.SetBool(targetBool1, status1);
        animator.SetBool(targetBool2, status2);
        animator.SetBool(targetBool3, status3);
    }

}

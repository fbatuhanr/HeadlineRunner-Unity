using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public bool isRunningEnable;
    
    private void Start()
    {
        isRunningEnable = true;
        animator = GetComponent<Animator>();
    }

    public void PlayerMovementAnimationBySpeed(float currentSpeed)
    {
        if (isRunningEnable)
        {
            if (currentSpeed > 5f)
            {
                animator.SetBool("isFastRunning", true);
            }
            else if (currentSpeed > 2.5f)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isFastRunning", false);
            }
            else if (currentSpeed > 0f)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    public void PlayAnimation(string stateName)
    {
        animator.Play(stateName);
    }

    public void SetAnimBool(string name, bool value)
    {
        animator.SetBool(name,value);
    }
}

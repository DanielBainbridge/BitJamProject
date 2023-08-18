using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float launchDistance = 3;
    public float launchTime = 0.5f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LaunchPlayer(PlayerController pC)
    {
        Vector2 totalMovement = Vector2.zero;
        totalMovement.y = Mathf.Sqrt(-2f * pC.gravity * launchDistance);


        //pC.AddVelocity(totalMovement);
        pC.velocity.y = totalMovement.y;
        PlayAnimation();
        Invoke("ResetAnimationTrigger", launchTime);

    }


    public void PlayAnimation()
    {
        animator.SetTrigger("Spring");
    }

    public void ResetAnimationTrigger()
    {
        animator.ResetTrigger("Spring");
    }
}

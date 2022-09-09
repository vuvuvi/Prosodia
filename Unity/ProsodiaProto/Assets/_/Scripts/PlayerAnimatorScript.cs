using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    public Animator Animator;
    public CharacterMovement CharacterMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterMovement.Iwalk)
        {
            Animator.SetBool("IsWalking", true);
        }else Animator.SetBool("IsWalking", false);
    }
}

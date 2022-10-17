using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAction : ActionHolder
{
    public Animator Animator;
    public string AnimationName;

    private void Animate()
    {
        Animator.Play(AnimationName);
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            Animate();
        };
    }

    private void Update()
    {
    }
}
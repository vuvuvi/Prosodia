using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAlien : MonoBehaviour
{
    public float Intensity;
    public AnimationTime Animate;
    public Transform TransformRef;
    public float VerticalOffset;
    public Transform MoveTo;
    public GameObject Armature;
    
    public void UpdateAnimation(float time)
    {
        float progress = time / Animate.Duration;
        Vector3 position = transform.position;
        position.y = VerticalOffset + Mathf.Sin(2 * Mathf.PI * progress) * Intensity;
        TransformRef.position = position;
    }

    public void StopAnimation(float time)
    {
        Animate.StartAnimation();
    }

    public void MoveAlien()
    {
        Armature.transform.position = MoveTo.position;
        Armature.transform.rotation = MoveTo.rotation;
    }
}

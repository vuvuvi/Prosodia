using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPingPlayer : MonoBehaviour
{
    public Material shader;
    public AnimationTime animation;

    public void StartAnimation()
    {
        if(animation.State != StateAnime.STARTED)
        {
            animation.StartAnimation();
        }
    }


    public void UpdateAnimation(float currentTime)
    {
        float time = currentTime / animation.Duration ;

        float radius = 5.5f * time;
        float transparency = Mathf.Sin(time * 2 * Mathf.PI);

        shader.SetFloat("_Radius", radius);
        shader.SetFloat("_Transparency", transparency);
    }
}
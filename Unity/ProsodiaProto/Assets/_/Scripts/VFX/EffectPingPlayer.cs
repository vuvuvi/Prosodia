using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPingPlayer : MonoBehaviour
{
    public Material shader;
    public AnimationTime animate;

    public void StartAnimation()
    {
        if(animate.State != StateAnime.STARTED)
        {
            animate.StartAnimation();
        }
    }


    public void UpdateAnimation(float currentTime)
    {
        float time = currentTime / animate.Duration ;

        float radius = 5.5f * time;
        float transparency = Mathf.Sin(time * 2 * Mathf.PI);

        shader.SetFloat("_Radius", radius);
        shader.SetFloat("_Transparency", transparency);
    }
}
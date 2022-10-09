using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPingPlayer : MonoBehaviour
{
    public Material shader;
    public AnimationTime animate;
    
    public float radius = 5.5f;
    public float strokeBlur = .25f;

    private void Start()
    {
        shader.SetFloat("_Transparency", 0);
    }

    public void StartAnimation()
    {
        if(animate.State != StateAnime.STARTED)
        {
            animate.StartAnimation();
        }
    }


    public void UpdateAnimation(float currentTime)
    {
        float progress = currentTime / animate.Duration;
        
        float transparency = Mathf.Sin(progress * Mathf.PI);

        shader.SetFloat("_Radius", radius * progress);
        shader.SetFloat("_Transparency", transparency);
        shader.SetFloat("_Stroke_Blur", strokeBlur + progress);
    }
}
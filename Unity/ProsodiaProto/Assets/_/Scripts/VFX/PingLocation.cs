using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingLocation : MonoBehaviour
{
    public Material shader;
    public AnimationTime animate;

    private float maxRadius = 4.5f;
    
    [Range(0, 1)]
    public float TransparencyLimit;
    
    [Range(0, 1)]
    public float RadiusLimit;
    public bool toShow;

    private void Start()
    {
        shader.SetFloat("_Transparency", 0);
    }

    public void StartAnimation()
    {
        if(!toShow)
        {
            toShow = true;
            animate.StartAnimation();
        }
    }

    public void SetColor(Color color)
    {
        shader.color = color;
    }

    public void UpdateAnimation(float currentTime)
    {
        float progress = currentTime / animate.Duration;

        float radius;
        float transparency;

        if(toShow)
        {
            radius = maxRadius * Mathf.Sqrt(progress);
            transparency = Mathf.Sqrt(progress);
        }
        else
        {
            radius = maxRadius + Mathf.Sin(progress * 2 * Mathf.PI) * RadiusLimit;
            transparency = .5f + Mathf.Sin(progress * 2 * Mathf.PI) * TransparencyLimit/2;
        }

        shader.SetFloat("_Radius", radius);
        shader.SetFloat("_Transparency", transparency);
    }

    public void StopedAnimation(float currenTime)
    {
        toShow = false;
        animate.StartAnimation();
    }
}
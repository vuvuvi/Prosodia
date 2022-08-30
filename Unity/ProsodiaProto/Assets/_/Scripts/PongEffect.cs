using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEffect : MonoBehaviour
{
    public float size;
    public float duration;
    public float time;
    public Material shader;
    public bool started;
    
    void Update()
    {
        if(started && time < duration)
        {
            time += Time.deltaTime;
            transform.localScale += size * (Vector3.one * Time.deltaTime)/duration;
            shader.SetFloat("_transparency", ((1 - time/duration)*2));
        }
    }

    public void StartAnimation()
    {
        time = 0;
        shader.SetFloat("_transparency", 1f);
        transform.localScale = Vector3.zero;
        started = true;
    }
}
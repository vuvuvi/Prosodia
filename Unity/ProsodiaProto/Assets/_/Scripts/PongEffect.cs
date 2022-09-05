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
            Vector3 localScale = transform.localScale;
            shader.SetFloat("_transparency", ((1 - time/duration)*2));

            Debug.Log((1 - time/duration)*2);
        }
        else
        {
            Reset();
        }
    }

    public void Reset()
    {
        transform.localScale = Vector3.zero;
        shader.SetFloat("_transparency", 2f);
    }

    public void StartAnimation()
    {
        Reset();
        time = 0;
        started = true;
    }
}
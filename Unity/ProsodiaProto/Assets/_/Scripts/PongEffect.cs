using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEffect : MonoBehaviour
{
    public float duration;
    public float time;
    public Material shader;

    void Start()
    {
        time = 0;
    }

    
    void Update()
    {
        if(time >= duration)
        {
            time = 0;
            shader.SetFloat("_transparency", 1f);
            transform.localScale = Vector3.zero;
        }
        else
        {
            time += Time.deltaTime;
            transform.localScale += (Vector3.one * Time.deltaTime)/duration;
            shader.SetFloat("_transparency", (1 - time/duration));
        }

        Debug.Log(shader.GetFloat("_transparency"));
    }
}

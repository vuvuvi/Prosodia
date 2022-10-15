using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFontain : NoteActionHolder
{
    public ParticleSystem ParticleSystem;
    public float StartSpeed;
    public float LitUpTime = 1;

    private void Start()
    {
        CopyMaterial();
        ParticleSystem.Stop();
    }

    protected void CopyMaterial()
    {
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        ParticleSystemRenderer renderer = ParticleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(renderer.material);
    }

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        ParticleSystem.name = "PS" + note;
        ParticleSystem.MainModule main = ParticleSystem.main;
        main.startSpeedMultiplier = StartSpeed + note;
        ParticleSystem.Play();
        StartCoroutine(LightDownCoroutine());
    }

    private IEnumerator LightDownCoroutine()
    {
        yield return new WaitForSeconds(LitUpTime);
        LightDown();
        yield return null;
    }

    private void LightDown()
    {
        if (!IsValid)
        {
            ParticleSystem.MainModule main = ParticleSystem.main;
            main.startSpeedMultiplier = 0;
            ParticleSystem.Stop();
        }
    }

    protected override void Validate()
    {
        LightDown();
    }
}
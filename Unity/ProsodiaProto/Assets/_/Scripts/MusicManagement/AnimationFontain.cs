using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFontain : NoteActionHolder
{
    public ParticleSystem ParticleSystemRef;
    public Material MaterialShader;
    public float StartSpeed;
    public float LitUpTime = 1;

    private void Start()
    {
        ParticleSystemRef.Stop();
    }

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        ParticleSystemRef.startSpeed = StartSpeed + note;
        ParticleSystemRef.Play();
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
            ParticleSystemRef.startSpeed = 0;
            ParticleSystemRef.Stop();
        }
    }

    protected override void Validate()
    {
        LightDown();
    }
}

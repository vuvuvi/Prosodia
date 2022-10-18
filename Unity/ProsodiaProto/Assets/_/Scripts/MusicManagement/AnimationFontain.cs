using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFontain : NoteActionHolder
{
    public ParticleSystem ParticleSystem;
    public float StartSpeed;
    public float LitUpTime = 1;
    private Material material;

    private void Start()
    {
        CopyMaterial();
        ParticleSystem.Stop();
    }

    protected void CopyMaterial()
    {
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        ParticleSystemRenderer renderer = ParticleSystem.GetComponent<ParticleSystemRenderer>();
        material = new Material(renderer.material);
        renderer.material = material;
    }

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        //ne fonctionne pas
        //var color = NoteInfoProvider.GetNoteColor(note);
        //material.color = color;
        //material.SetColor("_Color", color);
        //main.startColor = color;
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
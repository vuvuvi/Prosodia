using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNote : NoteActionHolder
{
    public Material Material;
    public float LitUpTime = 1;

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        var newColor = NoteInfoProvider.GetNoteColor(note);
        GetComponentInChildren<MeshRenderer>().material = new Material(Material);
        Material.SetColor("_Color", newColor);
        GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", newColor);
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
        }
    }

    protected override void Validate()
    {
        LightDown();
    }
}

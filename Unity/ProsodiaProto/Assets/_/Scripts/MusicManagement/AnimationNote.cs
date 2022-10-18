using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNote : NoteActionHolder
{
    public Material Material;
    public float LitUpTime = 1;

    private void Start()
    {
        Material = new Material(GetComponentInChildren<MeshRenderer>().material);
    }

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        var newColor = NoteInfoProvider.GetNoteColor(note);

        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        Material.SetColor("_EmissionColor", newColor);
        Material[] materials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < materials.Length; i++)
            materials[i] = Material;
        meshRenderer.materials = materials;
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

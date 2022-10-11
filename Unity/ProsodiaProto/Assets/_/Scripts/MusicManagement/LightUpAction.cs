using System;
using System.Collections;
using UnityEngine;

public class LightUpAction : NoteActionHolder
{
    public Material EmissiveMaterial;
    private Material baseMaterial;
    private MeshRenderer meshRenderer;
    public GameObject ParticuleSystem;

    public float LitUpTime = 1f;

    private void OnEnable()
    {
        Action = LightUp;
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        baseMaterial = new Material(meshRenderer.material);
        meshRenderer.material = baseMaterial;
        ParticuleSystem.SetActive(false);

    }
    protected override void Validate()
    {
        LightDown();
    }

    private void LightDown()
    {
        if (IsValid) return;
        ParticuleSystem.SetActive(false);
        meshRenderer.material = baseMaterial;
    }

    private void LightUp(int note)
    {
        ParticuleSystem.SetActive(true);
        meshRenderer.material = EmissiveMaterial;
        StartCoroutine(LightDownCoroutine());
    }
    private IEnumerator LightDownCoroutine()
    {
        yield return new WaitForSeconds(LitUpTime);
        LightDown();
        yield return null;
    }
}
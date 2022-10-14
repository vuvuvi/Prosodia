using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBuble : NoteActionHolder
{
    public Material MaterialShader;
    public Transform To;
    public Transform Initial;
    public Transform Visual;
    public float LitUpTime = 1;

    protected void OnEnable()
    {
        Action = NotePressed;
    }

    private void NotePressed(int note)
    {
        var newColor = NoteInfoProvider.GetNoteColor(note);
        GetComponentInChildren<MeshRenderer>().material = MaterialShader;
        Visual.transform.localScale = To.localScale;
        Visual.transform.localPosition = To.localPosition;
        Visual.transform.localRotation = To.localRotation;
        MaterialShader.SetColor("_Color", newColor);
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
            Visual.transform.localPosition = Initial.localPosition;
            Visual.transform.localRotation = Initial.localRotation;
            Visual.transform.localScale = Initial.localScale;
        }
    }

    protected override void Validate()
    {
        LightDown();
    }
}

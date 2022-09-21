using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpAction : NoteActionHolder
{
  private Material material;
  private MeshRenderer meshRenderer;
  private Color baseColor;
  public float LitUpTime = 1;
  protected void OnEnable()
  {
    Action = LightUp;
    meshRenderer = gameObject.GetComponent<MeshRenderer>();
    material = new Material(meshRenderer.material);
    meshRenderer.material = material;
    baseColor = material.color;
  }
  private void LightUp(int note)
  {
    var newColor = NoteInfoProvider.GetNoteColor(note);
    if (material.color == newColor)
    {
      return;
    }
    material.color = newColor;
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
      material.color = baseColor;
  }
  protected override void Validate()
  {
    LightDown();
  }
}

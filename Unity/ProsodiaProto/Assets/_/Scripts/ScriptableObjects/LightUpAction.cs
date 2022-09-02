using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "So/Actions/LightUp")]
public class LightUpAction : ActionHolder
{
  public NoteInfoProvider noteInfoProvider;
  private void OnEnable()
  {
    Action = LightUp;
  }
  private void LightUp(GameObject go, int note)
  {
    var meshrenderer = go.GetComponent<MeshRenderer>();
    var oldMat = meshrenderer.material;
    var newColor = noteInfoProvider.GetNoteColor(note);
    if(oldMat.color == newColor)
    {
      return;
    }
    NoteListener mb = go.GetComponent<NoteListener>();
    meshrenderer.material = new Material(meshrenderer.material);
    meshrenderer.material.color = newColor;
    Debug.Log($"{go.name} lights up for note{note}");
    mb.StartCoroutine(LightDown(meshrenderer, oldMat.color));
  }

  private IEnumerator LightDown(MeshRenderer mesh, Color color)
  {
    yield return new WaitForSeconds(1);
    mesh.material.color = color;
    yield return null;
  }

}

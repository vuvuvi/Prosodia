using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeColorAction : NoteActionHolder
{
    private List<Material> materials;
    private MeshRenderer meshRenderer;
    private List<Color> baseColors;
    public float LitUpTime = 1;

    protected void OnEnable()
    {
        Action = LightUp;
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        materials = new List<Material>();
        baseColors = new List<Color>();
        var mats = meshRenderer.materials.ToList();
        foreach (var mat in mats)
        {
            materials.Add(new Material(mat));
            baseColors.Add(mat.color);
        }
        meshRenderer.materials = materials.ToArray();
    }
    private void LightUp(int note)
    {
        var newColor = NoteInfoProvider.GetNoteColor(note);
        if (materials[0].color == newColor)
        {
            return;
        }
        materials.ForEach(m => m.color = newColor);
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
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].color = baseColors[i];
            }
    }
    protected override void Validate()
    {
        LightDown();
    }
}

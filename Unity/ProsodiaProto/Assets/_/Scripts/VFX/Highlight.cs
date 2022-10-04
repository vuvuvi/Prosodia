using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Material material;
    public GameObject instanceHighlight;
    private void OnValidate()
    {
        material = Resources.Load<Material>("Ping");
        
        Compute();
    }

    public void Compute()
    {
        if(!material || instanceHighlight)return;
        instanceHighlight = new GameObject($"{name}_highlight");

        HighlightCheck highlightCheck = instanceHighlight.AddComponent<HighlightCheck>();
        highlightCheck.highlight = this;
        highlightCheck.check = true;

        instanceHighlight.AddComponent<MeshFilter>().mesh = transform.GetComponentInChildren<MeshFilter>().mesh;
        
        MeshRenderer meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
        List<Material> materials = new List<Material>();
        foreach (var mat in meshRenderer.materials)
        {
            materials.Add(material);
        }
        instanceHighlight.AddComponent<MeshRenderer>().materials = materials.ToArray();
        
        instanceHighlight.transform.parent = transform;
        instanceHighlight.transform.position = meshRenderer.transform.position;
        instanceHighlight.transform.rotation = meshRenderer.transform.rotation;
        instanceHighlight.transform.localScale = meshRenderer.transform.localScale;
    }

    private void OnDestroy()
    {
        if(instanceHighlight)
        {
            Destroy(instanceHighlight);
        }
        else
        {
            Debug.LogWarning("Mmmm, it's weird why the Highlight Game Object is already destroy ?");
        }
    }
}
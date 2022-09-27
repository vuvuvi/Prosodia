using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceScript : MonoBehaviour
{
    public GameObject toReplace;

    public bool replaced = false;

    private void OnDrawGizmos() 
    {
        if(replaced) return;

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        
        foreach (var go in gameObjects)
        {
            int v = go.name.IndexOf("Outer_Sanctuary_Piece");
        }

        replaced = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceScript : MonoBehaviour
{
    //public GameObject prefab;

    //public bool replaced = false;

    //public string pattern = "Outer_Sanctuary_Piece";

    //private void OnDrawGizmos() 
    //{
    //    throw new System.Exception("IF ARE YOUR SURE HOW USE THIS SCRIPT, COMMENT THIS LINE");

    //    if(replaced) return;

    //    GameObject[] gameObjects = FindObjectsOfType<GameObject>();

    //    int count = 0;

    //    Debug.Log("objects" + gameObjects.Length);
    //    Debug.Log($"Prefab <<{prefab.name}>> replace with {pattern}");
                
    //    foreach (var go in gameObjects)
    //    {
    //        if(go.name.IndexOf(pattern) != -1)
    //        {
    //            StartCoroutine(ReplaceWithPrefab(go));
    //            count++;
    //        }
    //    }

    //    Debug.Log("replaced" + count);

    //    replaced = true;

    //    Debug.Log("Finish");
    //}

    //public IEnumerator ReplaceWithPrefab(GameObject go)
//    {
//#if UNITY_EDITOR
//        Object @object = UnityEditor.PrefabUtility.InstantiatePrefab(prefab, go.transform.parent);
//        GameObject instance = @object as GameObject;
//        instance.name = prefab.name;
//        instance.transform.position = go.transform.position;
//        instance.transform.rotation = go.transform.rotation;
//        instance.transform.localScale = go.transform.localScale;
//        Debug.Log($"Go <<{go.name}>> into <<{go.transform.parent.name}>>");
//        yield return new WaitForSeconds(.1f);
//        Debug.Log($"Destroy {go}");
//        DestroyImmediate(go); 
//#endif
//    }
}

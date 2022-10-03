using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightCheck : MonoBehaviour
{
    public Highlight highlight;
    public bool check;


    private void Update()
    {
        CheckOrRemove();
    }

    private void OnDrawGizmos()
    {
        CheckOrRemove();
    }

    public void CheckOrRemove()
    {
        if(check && !highlight)
        {
            StartCoroutine(Remove());
        }
    }

    public IEnumerator Remove()
    {
        yield return new WaitForSeconds(.1f);
        DestroyImmediate(gameObject);
        check = false;
    }
}
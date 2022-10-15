using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public string NameScene;
    public float timeOutLoadScene;
    
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(timeOutLoadScene);
        SceneManager.LoadSceneAsync(NameScene, LoadSceneMode.Additive);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string NameSceneToLoad;

    public void RestartGame()
    {
        SceneManager.LoadScene(NameSceneToLoad);
    }
}

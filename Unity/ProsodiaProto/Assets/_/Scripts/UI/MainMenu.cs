using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public string FirstLevelName;
  public string CreditsSceneName;

  public void StartGame()
  {
    SceneManager.LoadScene(FirstLevelName);
  }

  public void OpenOptions()
  {

  }
  public void CloseOptions()
  {

  }

  public void GoToCredits()
  {
    SceneManager.LoadScene(CreditsSceneName);
  }
  public void QuitGame()
  {
    Application.Quit();
    Debug.Log("Quitting");
  }
}

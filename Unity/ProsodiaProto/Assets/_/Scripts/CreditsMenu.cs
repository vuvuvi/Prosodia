using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
  public string MenuScene = "Menu";

  public void ReturnToMainMenu()
  {
    SceneManager.LoadScene(MenuScene);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public string FirstLevelName;
  public string CreditsSceneName;
  public Material[] pingButtons;
  public AnimationTime animate;

  public float StrokeBlur;
  public float Transparency;

  private void Start()
  {
    ResetAnimation();
  }

  public void StartGame()
  {
    SceneManager.LoadScene(FirstLevelName);
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

  public void ResetAnimation()
  {
    foreach (var ping in pingButtons)
    {
       ping.SetFloat("_StrokeBlur", StrokeBlur);
       ping.SetFloat("Transparency", Transparency);
    }
    animate.StartAnimation();
  }

  public void AnimatePing(float time)
  {
    float progress = time/animate.Duration;
    
    float radius = Mathf.Sin(progress * 2 * Mathf.PI) + 4;
    float transparency = Mathf.Sin(progress * 2 * Mathf.PI) * .25f + .5f;

    foreach (var ping in pingButtons)
    {
      ping.SetFloat("_Radius", radius);
      ping.SetFloat("_Transparency", transparency);
    }
  }

  public void AnimateEnd(float time)
  {
    ResetAnimation();
  }
}
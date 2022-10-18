using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FinishGameAction : ActionHolder
{
    public GameObject Camera;
    public string CreditSceneName;
    public bool GameIsFinished;
    public float TimeOutShowCredit;
    public Overlay Overlay;
    
    private void FinishGame()
    {
        GetComponent<CameraManager>().enabled = false;
        GetComponent<CharacterMovement>().enabled = false;
        GameIsFinished = true;
        transform.parent = Puzzle.transform;
        Camera.transform.parent = transform;
    }

    private void Update()
    {
        if(GameIsFinished && PathReached())
        {
            GameIsFinished = false;
            Overlay.toHidde = false;
            Overlay.animate.Duration = 5;
            Overlay.animate.StartAnimation();
            StartCoroutine(LoadScene());
        }
    }

    protected IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(TimeOutShowCredit);
        SceneManager.LoadScene(CreditSceneName);
    }

    protected bool PathReached()
    {
        NavMeshAgent agent = GetComponent<MoveObjectAction>().ObjectToMove;
        float dist = agent.remainingDistance;
        return dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance==0;
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            FinishGame();
        };
    }
}
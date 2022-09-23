using UnityEngine;
using UnityEngine.AI;

public class MoveObjectAction : ActionHolder
{
    public NavMeshAgent ObjectToMove;
    public Transform Destination;

    private bool IsMoving;
    public float MoveSpeed;

    private void MoveObject()
    {
        IsMoving = true;
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            MoveObject();
        };
    }
    private void Update()
    {
        if(IsMoving)
        {
            ObjectToMove.SetDestination( Destination.position); 
        }
    }
}

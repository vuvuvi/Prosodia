using UnityEngine;
using UnityEngine.AI;

public class MoveObjectAction : ActionHolder
{
    public NavMeshAgent ObjectToMove;
    public Transform Destination;

    public float MoveSpeed;

    private void MoveObject()
    {
        ObjectToMove.speed = MoveSpeed;
        ObjectToMove.SetDestination(Destination.position);
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            MoveObject();
        };
    }
}

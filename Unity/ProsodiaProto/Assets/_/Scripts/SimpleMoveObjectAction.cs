using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObjectAction : ActionHolder
{
    public Transform ObjectToMove;
    public Transform Destination;
    public float Speed = 1;
    private bool isMoving;

    private void Move()
    {
        isMoving = true;
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            Move();
        };
    }
    private void Update()
    {
        if (Vector3.Distance(ObjectToMove.position, Destination.position) < 0.2f)
            return;
        if (isMoving)
        {
            var movement = Destination.position - ObjectToMove.position;
            ObjectToMove.position += movement.normalized * Time.deltaTime * Speed;
        }
    }
}
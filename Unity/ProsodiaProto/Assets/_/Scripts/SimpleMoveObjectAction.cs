using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObjectAction : NoteActionHolder
{
    public Transform ObjectToMove;
    public Transform Destination;
    public float Speed = 1;
    private bool isMoving;
    protected void OnEnable()
    {
        Action = Move;
    }
    protected override void Validate()
    {

    }

    private void Move(int i)
    {
        isMoving = true;
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
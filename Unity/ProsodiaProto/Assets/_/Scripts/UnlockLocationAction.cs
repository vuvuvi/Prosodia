using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLocationAction : ActionHolder
{
    public Location LocationToUnlock;

    private void Unlock()
    {
        LocationToUnlock.IsAvailable = true;
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            Unlock();
        };
    }
}

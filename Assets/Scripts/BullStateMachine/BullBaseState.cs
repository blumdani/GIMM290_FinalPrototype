using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BullBaseState 
{
    public abstract void EnterState(BullStateManager bull);

    public abstract void UpdateState(BullStateManager bull);

    public abstract void OnCollisionEnter(BullStateManager bull, Collision collision);
}

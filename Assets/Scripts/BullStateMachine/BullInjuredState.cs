using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullInjuredState : BullBaseState
{
    //Grab player transform to use for rotation
    public Transform target;
    public float rotationSpeed = 3.0f;

    public override void EnterState(BullStateManager bull)
    {
       //Stop bull animations, look at player. TODO: Add animation for bull to lean forward
       target = GameObject.FindGameObjectWithTag("Player").transform; 
       Debug.Log("Entered Injured state");
       bull.dustFront.Stop();
       bull.dustBack.Stop();

       Vector3 targetPosition = new Vector3(target.transform.position.x, bull.transform.position.y, target.transform.position.z);
       bull.transform.LookAt(targetPosition);
    }

    public override void UpdateState(BullStateManager bull)
    {
       //Nothing for update. The bull will remain stationary until the player attacks it.
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
       //No collision events in injured state
    }
}

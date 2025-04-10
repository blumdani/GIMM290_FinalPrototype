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
       target = GameObject.FindGameObjectWithTag("Player").transform; 
       Debug.Log("Entered Injured state");
       bull.dustFront.Stop();
       bull.dustBack.Stop();
    }

    public override void UpdateState(BullStateManager bull)
    {
       // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - bull.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(bull.transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(bull.transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        bull.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
       //No collision events in injured state
    }
}

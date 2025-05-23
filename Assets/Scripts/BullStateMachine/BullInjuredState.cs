using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullInjuredState : BullBaseState
{
    //Grab player transform to use for rotation
    public Transform target;
    public float rotationSpeed = 3.0f;

    private Animator animator;
    private bool bullTurned;
    private BullHealthController bhc;

    public override void EnterState(BullStateManager bull)
    {
      //Stop bull animations, look at player. TODO: Add animation for bull to lean forward
      target = GameObject.FindGameObjectWithTag("Player").transform; 
      Debug.Log("Entered Injured state");
      bull.dustFront.Stop();
      bull.dustBack.Stop();
      bullTurned = false;
      bull.StartCoroutine(BullTurn());
      animator = GameObject.FindGameObjectWithTag("Bull").GetComponent<Animator>();
      animator.SetBool("injured", true);
    }

    public override void UpdateState(BullStateManager bull)
    {
      //Keep the bull positioned at the player for first .25f. This fixes bugs with the bull colliding and facing weird directions, without it consistently turning.
      if(!bullTurned)
      {
         Vector3 targetPosition = new Vector3(target.transform.position.x, bull.transform.position.y, target.transform.position.z);
         bull.transform.LookAt(targetPosition);
      }
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
       //No collision events in injured state
    }

    public override void OnTriggerEnter(BullStateManager bull, Collider collider)
    {
        //No trigger events in this state
    }

    IEnumerator BullTurn()
    {
         yield return new WaitForSeconds(.25f);
         bullTurned = true;
    }
}

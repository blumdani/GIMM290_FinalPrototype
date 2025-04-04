using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BullResettingState : BullBaseState
{
    public bool isResetting;
    public GameObject player;

    private float delay;

    public override void EnterState(BullStateManager bull)
    {
        Debug.Log("Entered Resetting state");
        //Start cooldown on entering state
        isResetting = true;

        //Stop particle system and reset the prewarm counter
        bull.dustFront.Stop();
        bull.dustBack.Stop();
        delay = 0;
        
        //Reset bull velocity to zero and add a quick cooldown so the bull is not constantly moving.
        bull.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bull.StartCoroutine(InitialPause(bull));
        player = GameObject.FindGameObjectWithTag("Player");

        //Since the collider may have been temporarily disabled after collision and taken damage, make sure things are appropriately reset.
        player.GetComponent<CapsuleCollider>().enabled = true;
        BullCollidingState collidingstate = bull.collidingState;
        collidingstate.invincible = false;
    }
    
    //Starting state, or state when the bull has just finished a run (by colliding with the arena) and is about to charge again.
    public override void UpdateState(BullStateManager bull)
    {
        //Look at the player, do not apply new Y motion (keep the bull at the same height at all times)
        Vector3 targetPosition = new Vector3(player.transform.position.x, bull.transform.position.y, player.transform.position.z);
        bull.transform.LookAt(targetPosition);
        
        //Then move to charging
        if(isResetting == false)
        {
            //Once the initial pause is over, transition to the charging state
            bull.SwitchState(bull.chargingState);
        }


        
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
        //Collisions will only happen in colliding state
    }

    IEnumerator InitialPause(BullStateManager bull)
    {
        //random delay between 1 and 3 seconds
        System.Random rng = new System.Random();
        delay = rng.Next(1, 4);

        yield return new WaitForSeconds(delay);
        isResetting = false;

        bull.dustFront.Play();
        bull.dustBack.Play();
    }
}

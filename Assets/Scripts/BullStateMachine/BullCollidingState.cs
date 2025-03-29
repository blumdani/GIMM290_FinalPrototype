using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State for when the bull is charging close to the player (and triggers a runthrough), or collides with the player, arena, or something else.
public class BullCollidingState : BullBaseState
{
    private Vector3 direction;
    private int speed;
    private bool approachingPlayer = true;

    public GameObject player;

    public override void EnterState(BullStateManager bull)
    {
        Debug.Log("Entered colliding state");
        //Match charging state speed.
        BullChargingState chargingState = bull.chargingState;
        speed = chargingState.speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void UpdateState(BullStateManager bull)
    {
        //Continue to move forward in the direction the bull is currently facing.
        bull.GetComponent<Rigidbody>().velocity = bull.transform.forward * speed;
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
        //Player collision
        if(collision.gameObject.tag == "Player")
        {
            //Damage Player (trigger invincibility, remove health, move through player correctly)
            player.GetComponent<CapsuleCollider>().enabled = false;
        }

        //Arena collision
        if(collision.gameObject.tag == "Arena")
        {
            bull.SwitchState(bull.resettingState);
        }

        if(collision.gameObject.tag == "Target")
        {
            //Make target inactive
            collision.gameObject.SetActive(false);

            //To Do: In a target manager, reduce the number of targets by 1 and check if all targets were made inactive.
        }
    }
}

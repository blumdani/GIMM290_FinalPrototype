using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State for when the bull is charging close to the player (and triggers a runthrough), or collides with the player, arena, or something else.
public class BullCollidingState : BullBaseState
{
    private Vector3 direction;
    private int speed;
    private bool approachingPlayer = true;

    //Health management
    private HealthController hc;
    private BullHealthController bhc;
    public bool invincible;
    public GameObject player;

    public override void EnterState(BullStateManager bull)
    {
        Debug.Log("Entered colliding state");
        //Match charging state speed.
        BullChargingState chargingState = bull.chargingState;
        speed = chargingState.speed;

        hc = GameObject.Find("HealthController").GetComponent<HealthController>();
        bhc = GameObject.Find("Bull").GetComponent<BullHealthController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void UpdateState(BullStateManager bull)
    {
        //Keep particle system active until the bull stops
        if (!bull.dustFront.isPlaying)
        {
           bull.dustFront.Play();
        }
        if (!bull.dustBack.isPlaying)
        {
            bull.dustBack.Play();
        }

        //Continue to move forward in the direction the bull is currently facing.
        bull.GetComponent<Rigidbody>().velocity = bull.transform.forward * speed;
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
        //Player collision
        if(collision.gameObject.tag == "Player")
        {
            //Damage Player (trigger invincibility, remove health, move through player correctly)
            if(!invincible)
            {
                Debug.Log("Player hit");
                hc.TakeDamage();
                invincible = true;
            }
            player.GetComponent<CapsuleCollider>().enabled = false;

            //When player is still, the bull will pass underneath it and the player will fly in the air. For now, briefly disable the bull collider as well until the player collider is disabled.
            bull.StartCoroutine(BullColliderDisable(bull));
        }

        //Arena collision
        if(collision.gameObject.tag == "Arena")
        {
            bull.SwitchState(bull.resettingState);
            bhc.BullDamage();
            if(BullHealthController.health <= 0)
            {
                bull.SwitchState(bull.injuredState);
            }
        }

        if(collision.gameObject.tag == "Target")
        {
            //Make target inactive
            collision.gameObject.SetActive(false);

            //To Do: In a target manager, reduce the number of targets by 1 and check if all targets were made inactive.
        }
    }

    IEnumerator BullColliderDisable(BullStateManager bull)
    {
        bull.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(.05f);
        bull.GetComponent<BoxCollider>().enabled = true;
    }
}

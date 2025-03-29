using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BullChargingState : BullBaseState
{
    private Vector3 direction;
    public int speed;

    public GameObject player;
    private float distance;
    [SerializeField] private float runthroughDistance = 75f;
    
    //State where bull is charging toward the player but not close enough to trigger runthrough (and also has not collided with anything)
    public override void EnterState(BullStateManager bull)
    {
        Debug.Log("Entered charging state");
        //Set speed for the next charge
        System.Random rng = new System.Random();
        speed = rng.Next(150, 165);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void UpdateState(BullStateManager bull)
    {
        //Start running, update direction to continue to chase player
        direction = (player.transform.position - bull.transform.position).normalized;
        bull.GetComponent<Rigidbody>().velocity = direction * speed;

         //Keep the bull from applying any y-axis movement.
        Vector3 targetPosition = new Vector3(player.transform.position.x, bull.transform.position.y, player.transform.position.z);
        bull.transform.LookAt(targetPosition);

        //Set current distance between bull and player. When the bull is close enough, the bull will stop homing in on the player and run forward (and move to colliding state).
        distance = Vector3.Distance(bull.transform.position, player.transform.position);
        if(distance <= runthroughDistance)
        {
            bull.SwitchState(bull.collidingState);
        }
    }

    public override void OnCollisionEnter(BullStateManager bull, Collision collision)
    {
        //Collisions will only happen in colliding state
    }
}

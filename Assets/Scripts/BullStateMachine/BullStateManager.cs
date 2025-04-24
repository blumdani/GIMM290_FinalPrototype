using UnityEngine;
using TMPro;

public class BullStateManager : MonoBehaviour
{
    //State machine variables for the bull
    public BullBaseState currentState;
    public BullChargingState chargingState = new BullChargingState();
    public BullResettingState resettingState = new BullResettingState();
    public BullCollidingState collidingState = new BullCollidingState();
    public BullInjuredState injuredState = new BullInjuredState();

    // Public variables to be accessed by states, set in Unity
    public ParticleSystem dustFront;
    public ParticleSystem dustBack;
    

    void Start()
    {
        currentState = resettingState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);   
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }

    public void SwitchState(BullBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}

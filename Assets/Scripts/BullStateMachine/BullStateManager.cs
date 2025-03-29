using UnityEngine;
using TMPro;

public class BullStateManager : MonoBehaviour
{
    //State machine variables for the bull
    BullBaseState currentState;
    public BullChargingState chargingState = new BullChargingState();
    public BullResettingState resettingState = new BullResettingState();
    public BullCollidingState collidingState = new BullCollidingState();

    // Public variables to be accessed by states
    

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

    public void SwitchState(BullBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanic : MonoBehaviour
{
    FPSInput movementScript;

    public float dashSpeed;
    public float dashTime;
    private bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<FPSInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        float startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            movementScript.charController.Move(movementScript.movement * dashSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        canDash = true;
    }
}

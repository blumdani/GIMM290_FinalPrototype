using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashMechanic : MonoBehaviour
{
    FPSInput movementScript;

    public float dashSpeed;
    public float dashTime;
    private float dashCooldown = 1.5f;
    private bool canDash = true;
    public CaptureData cd;
    public TMP_Text timeText;

    public Image staminaBar;

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
        //Deplete stamina bar rapidly
        staminaBar.fillAmount = 0;

        canDash = false;
        float elapsedTime = 0f;

        //start refill during dash
        while(elapsedTime < dashTime)
        {
            elapsedTime += Time.deltaTime;
            movementScript.charController.Move(movementScript.movement * dashSpeed * Time.deltaTime);
            staminaBar.fillAmount = Mathf.Lerp(0, 1, elapsedTime / dashCooldown);
            yield return null;
        }

        //finish refill after dash
        while(elapsedTime < dashCooldown)
        {
            elapsedTime += Time.deltaTime;
            staminaBar.fillAmount = Mathf.Lerp(0, 1, elapsedTime / dashCooldown);
            yield return null;
        }
        staminaBar.fillAmount = 1;
        canDash = true;
    }
}

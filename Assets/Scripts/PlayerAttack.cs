using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    public BullStateManager bull;
    public TMP_Text textOverlay;

    private float distance;
    private bool attackable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Handle text overlay for attacking the bull
        if(bull.currentState == bull.injuredState)
        {
            textOverlay.text = "Approach the bull!";

            distance = Vector3.Distance(bull.transform.position, transform.position);

            if(distance <= 40f)
            {
                attackable = true;
                textOverlay.text = "Press F to attack!";
            }
        }

        //Attack the bull
        if(attackable && Input.GetKeyDown(KeyCode.F))
        {
            //Start animation and quicktime event. Temporary destroy for now
            Destroy(bull.gameObject);
        }
    }
}

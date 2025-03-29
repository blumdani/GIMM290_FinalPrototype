using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchStart : MonoBehaviour
{
    public TMP_Text leftClick;
    public GameObject timer;
    public GameObject bull;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.GetComponent<CharacterController>().enabled = true;
            bull.GetComponent<BullStateManager>().enabled = true;
            leftClick.text = "";
            timer.SetActive(true);
        }
    }
}

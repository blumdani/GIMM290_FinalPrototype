using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullHealthController : MonoBehaviour
{
    public static float health = 140;
    public Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BullDamage()
    {
        health -= 20;
        healthbar.fillAmount = health / 140;
    }
}

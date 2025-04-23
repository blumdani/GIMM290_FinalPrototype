using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullHealthController : MonoBehaviour
{
    public static float health = 20;
    public Image healthbar;

    public void BullDamage()
    {
        health -= 20;
        healthbar.fillAmount = health / 180;
    }
}

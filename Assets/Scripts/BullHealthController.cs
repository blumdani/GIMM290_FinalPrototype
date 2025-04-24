using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullHealthController : MonoBehaviour
{
    public static float health = 4;
    public Image healthbar;

    public void BullDamage()
    {
        health -= 1;
        healthbar.fillAmount = health / 4;
    }

    public float GetHealth()
    {
        return health;
    }
}

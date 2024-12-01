using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public static HealthController Instance;
    public Image healthbar;
    public static float health = 60;

    void Start()
    {
        health = 60;
        healthbar = GameObject.Find("HealthGreen").GetComponent<Image>();
    }

    void Update()
    {
        if(health <= 0)
        { 
            SceneManager.LoadScene("YouLost");
        }
    }

    public void TakeDamage()
    {
        health -= 20;
        healthbar.fillAmount = health / 60;
    }
}

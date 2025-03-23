using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour
{
    public static HealthController Instance;
    public Image healthbar;
    public static float health = 60;
    public CaptureData cd;
    public TMP_Text timeText;

    void Start()
    {
        health = 60;
        healthbar = GameObject.Find("HealthGreen").GetComponent<Image>();
    }

    void Update()
    {
        if(health <= 0)
        { 
            Scene scene = SceneManager.GetActiveScene();
            cd.SaveData("Lost all health in " + scene.name + " scene at " + timeText.text + "\n");
            SceneManager.LoadScene("YouLost");
        }
    }

    public void TakeDamage()
    {
        cd.SaveData("Took damage at " + timeText.text + "\n");
        health -= 20;
        healthbar.fillAmount = health / 60;
    }
}

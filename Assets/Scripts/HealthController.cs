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

    //For damage effect overlay
    public Image overlay;
    public float fadeSpeed = 1f;
    public float duration = .75f;
    private float durationTimer;

    void Start()
    {
        health = 60;
        healthbar = GameObject.Find("HealthGreen").GetComponent<Image>();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        if(health <= 0)
        { 
            Scene scene = SceneManager.GetActiveScene();
            cd.SaveData("Lost all health in " + scene.name + " scene at " + timeText.text + "\n");
            SceneManager.LoadScene("YouLost");
        }

        //For overlay effect
        if(overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= fadeSpeed * Time.deltaTime;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void TakeDamage()
    {
        cd.SaveData("Took damage at " + timeText.text + "\n");
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        health -= 20;
        healthbar.fillAmount = health / 60;
    }
}

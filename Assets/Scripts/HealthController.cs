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
    public float health = 4;
    public CaptureData cd;
    public TMP_Text timeText;

    //For damage effect overlay
    public Image overlay;
    public float fadeSpeed = .75f;
    public float duration = .5f;
    private float durationTimer;

    void Start()
    {
        healthbar = GameObject.Find("PlayerHealthGreen").GetComponent<Image>();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        if(health <= 0)
        { 
            Scene scene = SceneManager.GetActiveScene();
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
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        health -= 1;
        healthbar.fillAmount = health / 4;
    }
}

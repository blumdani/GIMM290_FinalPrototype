using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SelectLevel : MonoBehaviour
{
    public TextMeshProUGUI instructions;
    public Button controls;
    public TextMeshProUGUI exitGame;
    public Button playGame;

    // Start is called before the first frame update
    void Start()
    {
        LoadMainMenu();
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        if(this.gameObject.name == "PlayGame")
        {
            //Reset the bull health to 140
            BullHealthController.health = 140;
            SceneManager.LoadScene("TimedPrototype");
        }
        else if(this.gameObject.name == "Controls")
        {
            //Display Controls
            LoadControlsScreen();
        }
        else if(this.gameObject.name == "ExitGame")
        {
            if(exitGame.text == "Exit Game")
            {
                Application.Quit();
            }
            else if(exitGame.text == "Back to Main Menu")
            {
                LoadMainMenu();
            }
        }
    }

    private void LoadMainMenu()
    {
        instructions.text = "";
        controls.gameObject.SetActive(true);
        playGame.gameObject.SetActive(true);
        exitGame.text = "Exit Game";
        exitGame.fontSize = 20;
    }

    private void LoadControlsScreen()
    {
        instructions.text = "WASD - Move \nMouse - Look \nLeft Click - Quick Step \nF - Attack Bull (once injured)";
        controls.gameObject.SetActive(false);
        playGame.gameObject.SetActive(false);
        exitGame.text = "Back to Main Menu";
        exitGame.fontSize = 12;
    }
}

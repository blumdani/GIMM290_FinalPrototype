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
    private GameObject title;
    public TextMeshProUGUI backToMenu;
    public Button controls;
    public Button exitGame;
    public Button playGame;

    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.FindGameObjectWithTag("Title");
        LoadMainMenu();
        backToMenu.text = "";
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        if(this.gameObject.name == "PlayGame")
        {
            SceneManager.LoadScene("TimedPrototype");
        }
        else if(this.gameObject.name == "Controls")
        {
            //Display Controls
            LoadControlsScreen();
        }
        else if(this.gameObject.name == "ExitGame")
        {
            Application.Quit();
        }
        else if(this.gameObject.name == "BackToMenu")
        {
            //Display Main Menu
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        instructions.text = "";
        controls.gameObject.SetActive(true);
        playGame.gameObject.SetActive(true);
        exitGame.gameObject.SetActive(true);
        title.SetActive(true);
        backToMenu.gameObject.SetActive(false);
        backToMenu.text = "";
    }

    private void LoadControlsScreen()
    {
        instructions.text = "Move around the arena and dodge the bull. Guide the bull into the targets to injure it, then attack while it is injured. \n\n CONTROLS:\nWASD - Move\nMouse - Look\nLeft Click - Quick Step\nF - Attack Bull (once injured)";
        controls.gameObject.SetActive(false);
        playGame.gameObject.SetActive(false);
        exitGame.gameObject.SetActive(false);
        title.SetActive(false);
        backToMenu.gameObject.SetActive(true);
        backToMenu.text = "Back To Menu";
    }
}

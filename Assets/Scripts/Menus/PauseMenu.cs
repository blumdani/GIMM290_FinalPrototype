using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    private bool isPaused;
    private GameObject player;
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        player.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<FPSInput>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        player.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<FPSInput>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToControls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
        Debug.Log("Go to Controls Menu");
    }

    public void BackFromControls()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        Debug.Log("Back from Controls Menu");
    }
}

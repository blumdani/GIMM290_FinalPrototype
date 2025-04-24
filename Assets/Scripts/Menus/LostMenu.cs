using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostMenu : MonoBehaviour
{
    // Update is called once per frame
    public void OnButtonClick()
    {
        if(this.gameObject.name == "Retry")
        {
            SceneManager.LoadScene("Intro_Animation");
        }
        else if(this.gameObject.name == "ExitGame")
        {
            Application.Quit();
        }
    }
}

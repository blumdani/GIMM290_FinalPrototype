using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public CaptureData cd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        if(this.gameObject.name == "TimedMode")
        { 
            cd.SaveData("Selected Timed Mode from " + SceneManager.GetActiveScene().name + "\n");
            SceneManager.LoadScene("TimedPrototype");
        }
        else if(this.gameObject.name == "TargetMode")
        {
            cd.SaveData("Selected Target Mode from " + SceneManager.GetActiveScene().name + "\n");
            SceneManager.LoadScene("TargetPrototype");
        }
    }
}

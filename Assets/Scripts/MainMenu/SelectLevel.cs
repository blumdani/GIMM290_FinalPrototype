using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        if(this.gameObject.name == "TimedMode")
        { 
            SceneManager.LoadScene("TimedPrototype");
        }
        else if(this.gameObject.name == "TargetMode")
        {
            SceneManager.LoadScene("TargetPrototype");
        }
    }
}

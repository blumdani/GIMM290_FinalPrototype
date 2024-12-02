using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CaptureData : MonoBehaviour
{
    private string data;
    private string path = "";
    
    // Start is called before the first frame update
    void Start()
    {   
        path = Application.persistentDataPath + "/save.json";
    }

    public void SaveData(string data)
    {
        StreamWriter writer = new(path, append: true);
        writer.Write(data);
        writer.Close();
        Debug.Log(data);
    }

    
}

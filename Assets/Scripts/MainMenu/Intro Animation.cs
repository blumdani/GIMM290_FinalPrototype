using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroAnimation : MonoBehaviour
{
    public VideoPlayer video; // Reference to the VideoPlayer component
    private bool videoStarted = false; // Flag to check if the video has started

    // Start is called before the first frame update
    void Start()
    {
        video.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || (videoStarted == true && !video.isPlaying))
        {
            //If the player clicks, bypass the intro animation.
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        if(video.isPrepared && !video.isPlaying);
        {
            video.Play();
            videoStarted = true;
        }
    }
}

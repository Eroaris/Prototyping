using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour
{
    public GameObject sparksVideo;
    public GameObject startVideo;
    public int targetFrameRate = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup >= 1f)
        {
            sparksVideo.SetActive(true);
            startVideo.SetActive(false);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }

        
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour
{
    public CanvasGroup blackScreen;
    public int targetFrameRate = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    private void Start()
    {
        StartCoroutine(FadeOut(2f,3f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator FadeOut(float waitDuration, float fadeDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        
        for (float t = 0; t < fadeDuration; t+= Time.deltaTime)
        {
            blackScreen.alpha = Mathf.Lerp(1f,0f,t/ fadeDuration);
            
            yield return null;
        }
        blackScreen.alpha = 0;
    }
}

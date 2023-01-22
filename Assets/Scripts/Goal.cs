using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI goalText;
    Color lerpedColor;
    private SpriteRenderer _spriteRenderer;
    float goalTime = 5f; 
    float timePassed = 0;   
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void completeGoal()
    {
        timePassed += Time.deltaTime;
        
        float percentComplete = timePassed / goalTime;
        percentComplete = Mathf.Clamp01(percentComplete);
        lerpedColor = Color.Lerp(new Color(0.8584906f,0.004049493f,0.004049493f,0), new Color(0.8584906f,0.004049493f,0.004049493f,1), percentComplete);
        _spriteRenderer.color = lerpedColor;
        
    }
    
}

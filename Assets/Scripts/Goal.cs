using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI goalText;
    public GameStateManager GSM;
    
    Color lerpedColor;
    private SpriteRenderer _spriteRenderer;
    float goalTime = 60f; 
    float timePassed = 0;
    private bool playerInside;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerInside = false;
    }

    public void Update()
    {
        if (playerInside)
        {
            timePassed += Time.deltaTime;
        
            float percentComplete = timePassed / goalTime;
            percentComplete = Mathf.Clamp01(percentComplete);
            lerpedColor = Color.Lerp(new Color(0.07450981f,0.5450981f,0.9058824f,0), new Color(0.07450981f,0.5450981f,0.9058824f,1), percentComplete);
            _spriteRenderer.color = lerpedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}

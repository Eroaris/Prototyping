using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource _audioSource;
    public static BGM instance;
    public GameStateManager GSM;

    public AudioClip playingBGM;
    public AudioClip gameOverBGM;
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.Playing:
                _audioSource.Play();
                _audioSource.clip = playingBGM;
                break;
           case GameStateManager.GameState.Lose:
               _audioSource.clip = gameOverBGM;
               _audioSource.Play();
               break;

        }
    }
        private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

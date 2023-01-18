using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUi : MonoBehaviour
{
    public GameObject levelUPUI;

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
        
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        
    }
   
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.Playing:
                levelUPUI.SetActive(false);
                Time.timeScale = 1;
                break;
            
            case GameStateManager.GameState.LevelUP:
                levelUPUI.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
    void Start()
    {
       
    }
    
    void Update()
    {
        
    }

    public void chooseDamage()
    {
        
    }
}

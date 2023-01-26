using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
     public int targetFrameRate = 30;

     public static GameStateManager instance;
     public delegate void GameStateChangeDelegate(GameState targetState);
     public static event GameStateChangeDelegate OnGameStateChanged; 
    
     private GameState _currentState;
     
     private void Awake()
     {
         SetCurrentState(GameState.Playing);
         QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = targetFrameRate;
             
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
         public enum GameState
         {
             Start,
             Ready,
             Playing,
             LevelUP,
             Pause,
             Win,
             Lose, 
         }
         
         public GameState GetCurrentState()
         {
             return _currentState;
         }

         public void SetCurrentState(GameState targetState)
         {
             _currentState = targetState;

             if (OnGameStateChanged != null)
             {
                 OnGameStateChanged.Invoke(_currentState);
             }
         }

         private void Start()
         {
             SetCurrentState(GameState.Playing);
         } 
         public void ResumeGame()
         {
             OnGameStateChanged?.Invoke(GameState.Playing);
         }
         
}

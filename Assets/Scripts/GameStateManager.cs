using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
     public int targetFrameRate = 30;

     public static GameStateManager Instance;
     public delegate void GameStateChangeDelegate(GameState targetState);
     public static event GameStateChangeDelegate OnGameStateChanged; 
    
     private void Awake()
     {
         SetCurrentState(GameState.Playing);
         QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = targetFrameRate;
             
         if (Instance == null)
         {
             Instance = this;
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
    
         private GameState _currentState;

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
             SetCurrentState(GameState.Ready);
         }    
         
         
}

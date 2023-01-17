using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timertext;
    public float timeLimit = 0;
    public float tmpTime;
    int _minutes, _seconds;


    void Update()
    {
        tmpTime -= Time.deltaTime;
 
        _minutes = (int) tmpTime / 60;
 
        _seconds = (int) tmpTime % 60;
        
        timertext.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);

        if (tmpTime <= 0)
        {
            GameStateManager.Instance.SetCurrentState(GameStateManager.GameState.Lose);
        }
    }
    
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            
            case GameStateManager.GameState.Ready:
                Time.timeScale = 1;
                break;
            
            case GameStateManager.GameState.Playing:
                Time.timeScale = 1;
                break;
            
            case GameStateManager.GameState.Upgrade:
                Time.timeScale = 0;
                break;
                
            case GameStateManager.GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameStateManager.GameState.Lose:
                Time.timeScale = 0;
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
}
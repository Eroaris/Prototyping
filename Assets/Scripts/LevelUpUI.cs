using UnityEngine;
public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUPUI;
    public Player player;
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
                break;
            
            case GameStateManager.GameState.LevelUP:
                levelUPUI.SetActive(true);
                break;
        }
    }

}

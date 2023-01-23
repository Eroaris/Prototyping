using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XpBar : MonoBehaviour
{
    public GameStateManager GSM;
    private Player player;
    public Image mask;
    public float fillAmount;
   
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
            case GameStateManager.GameState.LevelUP:
                fillAmount = 0;
                break;
            
        }
    }
    void Awake()
    {
       player=GetComponentInParent<Player>();
    }
    void Update()
    {
        GetFill();
    }

    void GetFill()
    {
        fillAmount = player.currentXP / player.maxXP;
        mask.fillAmount = fillAmount;
    }
}

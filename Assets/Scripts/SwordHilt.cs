    using System;
    using UnityEngine;

public class SwordHilt : MonoBehaviour
{
    public GameStateManager GSM;
    public Player player;
    
    public float rangeIncrease = 0.1f;
    

    public void UpgradeRange()
    {
        if (player.canUpgrade)
        {
            transform.localScale = new Vector3(transform.localScale.x + rangeIncrease, transform.localScale.y + rangeIncrease,
                transform.localScale.z);
            player.canUpgrade = false;
            player.swordRange += rangeIncrease;
            GSM.SetCurrentState(GameStateManager.GameState.Playing);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XpBar : MonoBehaviour
{
  
    private Player player;
    public Image mask;
    public float fillAmount;
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
        if (player.currentXP >= player.maxXP)
        {
            fillAmount = 0;
        }
    }
}

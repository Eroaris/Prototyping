using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
     public int targetFrameRate = 30;
     
         private void Awake()
         {
             QualitySettings.vSyncCount = 0;
             Application.targetFrameRate = targetFrameRate;
         }
}

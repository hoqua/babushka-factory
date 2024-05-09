using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public GameObject upgradeOverlay;
   public void PauseGame()
   {
      Time.timeScale = 0;
      
   }

   public void ResumeGame()
   {
      Time.timeScale = 1;
   }

   public void ShowUpgradeOverlay()
   {
      upgradeOverlay.SetActive(true);
   }
   
   public void HideUpgradeOverlay()
   {
      upgradeOverlay.SetActive(false);
   }
}

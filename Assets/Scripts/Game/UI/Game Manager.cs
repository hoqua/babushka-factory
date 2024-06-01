using UnityEngine;

namespace Game.UI
{
   public class GameManager : MonoBehaviour
   {
      public GameObject upgradeOverlay;
      public CardManager cardManager;
      public Claw clawScript;
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
         cardManager.ShowUpgradeCards();
         
         clawScript.isClawSoundPlaying = false;
         clawScript.StopClawSound();
      }
   
      public void HideUpgradeOverlay()
      {
         upgradeOverlay.SetActive(false);
         
         clawScript.isClawSoundPlaying = true;
         clawScript.PlayClawSound();
         
      }
      
   }
}

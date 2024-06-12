using Features.Claw.Scripts;
using Resources.Cards.Scripts;
using UnityEngine;

namespace Game
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
         cardManager.BlockClawInput();
         cardManager.ShowUpgradeCards();
         
         clawScript.isClawSoundPlaying = false;
         clawScript.StopClawSound();
      }
   
      public void HideUpgradeOverlay()
      {
         upgradeOverlay.SetActive(false);
         cardManager.StartCoroutine(cardManager.UnblockClawInput(0.5f));

         clawScript.isClawSoundPlaying = false;
         clawScript.PlayClawSound();
      }
      
   }
}

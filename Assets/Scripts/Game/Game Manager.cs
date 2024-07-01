using Features.Claw.Scripts;
using Resources.Cards.Scripts;
using UnityEngine;

namespace Game
{
   public class GameManager : MonoBehaviour
   {
      public ClawAudioController clawAudioController;
      public GameObject upgradeOverlay;
      public CardManager cardManager;
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
         
         clawAudioController.isClawSoundPlaying = false;
         clawAudioController.StopClawSound();
      }
   
      public void HideUpgradeOverlay()
      {
         upgradeOverlay.SetActive(false);
         cardManager.StartCoroutine(cardManager.UnblockClawInput(0.5f));

         clawAudioController.isClawSoundPlaying = false;
         clawAudioController.PlayClawSound();
      }
      
   }
}

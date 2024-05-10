using UnityEngine;

namespace Game.UI
{
   public class GameManager : MonoBehaviour
   {
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
         cardManager.ShowUpgradeCards();
      }
   
      public void HideUpgradeOverlay()
      {
         upgradeOverlay.SetActive(false);
      }
   }
}

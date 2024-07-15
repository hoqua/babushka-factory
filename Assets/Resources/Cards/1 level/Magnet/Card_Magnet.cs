
using Features.Claw.Scripts;
using UnityEngine;

namespace Resources.Cards._1_level.Magnet
{
   public class CardMagnet : MonoBehaviour
   {

      public MagnetController magnetController;
      private void Awake()
      {
         magnetController = FindObjectOfType<MagnetController>();
      }

      //Добавляет клешне магнит, который прятигивает бабушек
      private void OnMouseDown()
      {
         if (magnetController.magnetEffect.enabled)
         {
            magnetController.UpgradeMagnet();
         }
                    
         magnetController.EnableMagnet();
      }
   }
}

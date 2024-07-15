using Resources.Effects.Projectile.Scripts;
using UnityEngine;

namespace Resources.Cards._1_level.Projectile
{
   public class CardProjectile : MonoBehaviour
   {
      public ProjectileSpawner projectileSpawnerScript;
      private void Awake()
      {
         projectileSpawnerScript = FindObjectOfType<ProjectileSpawner>();
      }

      //Спавнит "спутник" каждые 10 секунд. При попадании в бабушку замедляет её.
      //Повторное взяие уменьшает интервал на одну секунду.
      private void OnMouseDown()
      {
         if (projectileSpawnerScript.spawnInterval == 1) {return;}
         
         if (projectileSpawnerScript.enabled)
         {
            projectileSpawnerScript.spawnInterval -= 1;
            projectileSpawnerScript.RestartSpawningProjectiles();
         }
                    
         if (projectileSpawnerScript.enabled == false)
         {
            projectileSpawnerScript.enabled = true;
         }
      }
   }
}

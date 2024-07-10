using Resources.Effects.Spring_Wall.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Resources.Cards._1_level.SpringWall
{
    public class CardSpringWall : MonoBehaviour
    {

        public SpringWallSpawner springWallSpawnerScript;
        public SpringWallEffect springWallEffectScript;
        private void Awake()
        {
            springWallSpawnerScript = FindObjectOfType<SpringWallSpawner>();
            springWallEffectScript = FindObjectOfType<SpringWallEffect>();
        }
        
        //Призывает стену(-ы) по краям конвейера, которая отталкивает объекты
        private void OnMouseDown()
        {
            if (springWallSpawnerScript.isSpawnCoroutineActive && springWallSpawnerScript.spawnBothSides)
            {
                springWallEffectScript.springForce += 10f;
            }
                    
            if (springWallSpawnerScript.isSpawnCoroutineActive)
            {
                springWallSpawnerScript.spawnBothSides = true;
            }
                    
            springWallSpawnerScript.ActivateWallSpawn();
        }
    }
}

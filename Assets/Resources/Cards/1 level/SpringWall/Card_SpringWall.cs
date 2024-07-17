using Resources.Effects.Spring_Wall.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Resources.Cards._1_level.SpringWall
{
    public class CardSpringWall : MonoBehaviour
    {

        public SpringWallSpawner springWallSpawnerScript;
        private void Awake()
        {
            springWallSpawnerScript = FindObjectOfType<SpringWallSpawner>();
        }
        
        //Призывает стену(-ы) по краям конвейера, которая отталкивает объекты
        private void OnMouseDown()
        {
            if (springWallSpawnerScript.springForce > 150)
            {
                return; 
            }
            
            if (springWallSpawnerScript.isSpawnCoroutineActive && springWallSpawnerScript.spawnBothSides)
            {
                springWallSpawnerScript.springForce += 10f;
            }
                    
            if (springWallSpawnerScript.isSpawnCoroutineActive)
            {
                springWallSpawnerScript.spawnBothSides = true;
            }
                    
            springWallSpawnerScript.ActivateWallSpawn();
        }
    }
}

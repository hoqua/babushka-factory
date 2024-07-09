using UnityEngine;

namespace Resources.Effects.Spring_Wall.Scripts
{
    public class SpringWallSpawner : MonoBehaviour
    {
        public GameObject wallPrefab;
    
        public Vector2 spawnPointLeft;
        public Vector2 spawnPointRight;

        public void SpawnSpringWall()
        {
            Vector2 chosenSpawnPoint = Random.value < 0.5f ? spawnPointLeft : spawnPointRight;
            
            Instantiate(wallPrefab, chosenSpawnPoint, Quaternion.identity);
        }
    }
}

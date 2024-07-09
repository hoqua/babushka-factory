using System.Collections;
using UnityEngine;

namespace Resources.Effects.Spring_Wall.Scripts
{
    public class SpringWallSpawner : MonoBehaviour
    {
        public GameObject wallPrefab;
    
        public Vector2 spawnPointLeft;
        public Vector2 spawnPointRight;

        private float delayBeforeMoving = 30f;
        private float targetHeight = 12f;
        private float moveSpeed = 2.5f;

       
        public void SpawnOneSpringWall()
        {
            Vector2 chosenSpawnPoint = Random.value < 0.5f ? spawnPointLeft : spawnPointRight;
            
            GameObject spawnedWall = Instantiate(wallPrefab, chosenSpawnPoint, Quaternion.identity);
            
            StartCoroutine(MoveWallAfterDelay(spawnedWall, delayBeforeMoving, targetHeight, moveSpeed));
        }
        
        private IEnumerator MoveWallAfterDelay(GameObject wall, float delay, float targetHeight, float speed)
        {
            yield return new WaitForSeconds(delay);

            float targetY = wall.transform.position.y + targetHeight;
            Rigidbody2D rigidbody = wall.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            
            while (wall != null && wall.transform.position.y < targetY)
            {
                wall.transform.position += Vector3.up * (speed * Time.deltaTime);
                yield return null;
            }

            if (wall != null) 
            {
                Destroy(wall);
            }
        }
    }
}
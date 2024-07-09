using System.Collections;
using UnityEngine;

namespace Resources.Effects.Spring_Wall.Scripts
{
    public class SpringWallSpawner : MonoBehaviour
    {
        public GameObject wallPrefab;

        public Vector2 spawnPointLeft;
        public Vector2 spawnPointRight;

        private readonly float _delayBeforeMoving = 15f;
        private readonly float _spawnInterval = 30f;
        
        private readonly float _targetHeight = 12f;
        private readonly float _moveSpeed = 2.5f;

        private Coroutine _spawnCoroutine;
        
        public bool isSpawnCoroutineActive;
        public bool spawnBothSides;

        public void ActivateWallSpawn()
        {
            _spawnCoroutine ??= StartCoroutine(SpawnWallsRepeatedly());
            isSpawnCoroutineActive = true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnWallsRepeatedly()
        {
            while (true)
            {
                if (spawnBothSides)
                {
                    DestroyAllWalls();
                    SpawnTwoSpringWalls();
                }
                else
                {
                    SpawnOneSpringWall(); 
                }
               
                yield return new WaitForSeconds(_spawnInterval);
            }
            
        }

        private void SpawnOneSpringWall()
        {
            Vector2 chosenSpawnPoint = Random.value < 0.5f ? spawnPointLeft : spawnPointRight;

            GameObject spawnedWall = Instantiate(wallPrefab, chosenSpawnPoint, Quaternion.identity);

            StartCoroutine(MoveWallAfterDelay(spawnedWall, _delayBeforeMoving, _targetHeight, _moveSpeed));
        }

        private void SpawnTwoSpringWalls()
        {
            GameObject spawnedWallLeft = Instantiate(wallPrefab, spawnPointLeft, Quaternion.identity);
            GameObject spawnedWallRight = Instantiate(wallPrefab, spawnPointRight, Quaternion.identity);

            StartCoroutine(MoveWallAfterDelay(spawnedWallLeft, _delayBeforeMoving, _targetHeight, _moveSpeed));
            StartCoroutine(MoveWallAfterDelay(spawnedWallRight, _delayBeforeMoving, _targetHeight, _moveSpeed));
        }

        private IEnumerator MoveWallAfterDelay(GameObject wall, float delay, float targetHeight, float speed)
        {
            yield return new WaitForSeconds(delay);

            float targetY = wall.transform.position.y + targetHeight;
            
            Rigidbody2D rigidBody = wall.GetComponent<Rigidbody2D>();
            rigidBody.bodyType = RigidbodyType2D.Kinematic;

            var wallSpringEffect = wall.GetComponent<SpringEffect>();
            wallSpringEffect.enabled = false;
            
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

        private void DestroyAllWalls()
        {
            var walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (var wall in walls)
            {
                Destroy(wall);
            }
        }
    }
}

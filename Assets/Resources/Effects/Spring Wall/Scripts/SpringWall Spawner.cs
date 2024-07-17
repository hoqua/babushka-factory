using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Effects.Spring_Wall.Scripts
{
    public class SpringWallSpawner : MonoBehaviour
    {
        public GameObject wallPrefab;

        public Vector2 spawnPointLeft;
        public Vector2 spawnPointRight;

        private readonly float _spawnInterval = 5f;
        private readonly float _moveSpeed = 5f;
        private readonly float _moveDuration = 0.3f;
        private float _reverseDuration;
        
        public bool isSpawnCoroutineActive;
        public bool spawnBothSides;

        private void Start()
        {
            _reverseDuration = _moveDuration;
        }

        public void ActivateWallSpawn()
        {
            if (!isSpawnCoroutineActive)
            {
                StartCoroutine(SpawnWallsRepeatedly());
                isSpawnCoroutineActive = true;
            }
        }

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

            Vector3 initialMoveDirection = chosenSpawnPoint == spawnPointLeft ? Vector3.right : Vector3.left;
            Vector3 reverseMoveDirection = chosenSpawnPoint == spawnPointLeft ? Vector3.left : Vector3.right;

            GameObject spawnedWall = Instantiate(wallPrefab, chosenSpawnPoint, Quaternion.identity);
            StartCoroutine(MoveWall(spawnedWall, initialMoveDirection, reverseMoveDirection));
        }

        private void SpawnTwoSpringWalls()
        {
            GameObject spawnedWallLeft = Instantiate(wallPrefab, spawnPointLeft, Quaternion.identity);
            GameObject spawnedWallRight = Instantiate(wallPrefab, spawnPointRight, Quaternion.identity);

            StartCoroutine(MoveWall(spawnedWallLeft, Vector3.right, Vector3.left));
            StartCoroutine(MoveWall(spawnedWallRight, Vector3.left, Vector3.right));
        }

        private IEnumerator MoveWall(GameObject wall, Vector3 initialMoveDirection, Vector3 reverseMoveDirection)
        {
            float moveEndTime = Time.time + _moveDuration;
            while (wall != null && Time.time < moveEndTime)
            {
                wall.transform.position += initialMoveDirection * (_moveSpeed * Time.deltaTime);
                yield return null;
            }
            
            moveEndTime = Time.time + _reverseDuration;
            while (wall != null && Time.time < moveEndTime)
            {
                wall.transform.position += reverseMoveDirection * (_moveSpeed * Time.deltaTime);
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
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game.Effects
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public float spawnInterval = 2f;
        public GameObject projectilePrefab;

        private Camera _mainCamera;
        void Start()
        {
            _mainCamera = Camera.main;
            MoveSpawnPoint();
            InvokeRepeating(nameof(SpawnProjectile), spawnInterval, spawnInterval);
        }

        void SpawnProjectile()
        {
            var spawner = transform;
            Instantiate(projectilePrefab, spawner.position, spawner.rotation);
            MoveSpawnPoint();
        }

        void MoveSpawnPoint()
        {
            var newPosition = GetRandomPositionAlongTopWall();
            transform.position = newPosition;
            AdjustSpawnPointRotation();
        }

        private void AdjustSpawnPointRotation()
        {
            var cameraWidth = _mainCamera.aspect * _mainCamera.orthographicSize;
            var normalizedX = Mathf.InverseLerp(-cameraWidth, cameraWidth, transform.position.x);

            var angle = Mathf.Lerp(315, 225, normalizedX);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        Vector2 GetRandomPositionAlongTopWall()
        {
            var cameraHeight = _mainCamera.orthographicSize;
            var cameraWidth = _mainCamera.aspect * cameraHeight;
            
            var randomX = Random.Range(-cameraWidth, cameraWidth);
            return new Vector2(randomX, cameraHeight);
        }
        
        
        
    }
}

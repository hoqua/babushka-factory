using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public float spawnInterval = 10f;
        public GameObject projectilePrefab;

        public string obstacleTag = "Claw";
        public float checkRadius = 1f;
        
        private Camera _mainCamera;
        void Start()
        {
            _mainCamera = Camera.main;
            MoveSpawnPoint();
            InvokeRepeating(nameof(SpawnProjectile), spawnInterval, spawnInterval);
        }

        public void SpawnProjectile()
        {
            var spawner = transform;
            Instantiate(projectilePrefab, spawner.position, spawner.rotation);
            MoveSpawnPoint();
        }

        void MoveSpawnPoint()
        {
            Vector2 newPosition;
            do
            {
                newPosition = GetRandomPositionAlongTopWall();
            } while (IsPositionOccupied(newPosition));
            
            transform.position = newPosition;
            AdjustSpawnPointRotation();
        }

        bool IsPositionOccupied(Vector2 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, checkRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(obstacleTag))
                {
                    return true;
                }
            }

            return false;
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

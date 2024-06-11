using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class CameraBounds : MonoBehaviour
    {
        public Camera mainCamera;
        public string boundaryLayer = "Camera Boundary";
        public float thickness = 1f; // Толщина границы
        public float offset = 0.5f; // Смещение границы от краев видимости камеры
        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            CreateBound("TopBound", new Vector2(0, mainCamera!.orthographicSize + thickness / 2 + offset), new Vector2(mainCamera.aspect * mainCamera.orthographicSize * 2 + 2 * offset, thickness));
            CreateBound("BottomBound", new Vector2(0, -mainCamera.orthographicSize - thickness / 2 - offset), new Vector2(mainCamera.aspect * mainCamera.orthographicSize * 2 + 2 * offset, thickness));
            CreateBound("LeftBound", new Vector2(-mainCamera.aspect * mainCamera.orthographicSize - thickness / 2 - offset, 0), new Vector2(thickness, mainCamera.orthographicSize * 2 + 2 * offset));
            CreateBound("RightBound", new Vector2(mainCamera.aspect * mainCamera.orthographicSize + thickness / 2 + offset, 0), new Vector2(thickness, mainCamera.orthographicSize * 2 + 2 * offset));
        }

        void CreateBound(string name, Vector2 position, Vector2 size)
        {
            GameObject bound = new GameObject(name);
            bound.transform.position = position;
            bound.transform.parent = transform;
            BoxCollider2D boundCollider = bound.AddComponent<BoxCollider2D>();
            boundCollider.size = size;

            bound.layer = LayerMask.NameToLayer(boundaryLayer);
        }
    }
}
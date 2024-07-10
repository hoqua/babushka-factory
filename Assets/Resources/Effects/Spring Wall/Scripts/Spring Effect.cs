using UnityEngine;

namespace Resources.Effects.Spring_Wall.Scripts
{
    public class SpringWallEffect : MonoBehaviour
    {
   
        public float springForce = 30f;  // Сила отталкивания

        void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
            
                Vector2 collisionPoint = collision.contacts[0].point;
                Vector2 wallPosition = transform.position;

                float direction = collisionPoint.x < wallPosition.x ? -1 : 1; 
            
                Vector2 repelDirection = new Vector2(direction, 0); // Горизонтальное отталкивание
                rb.AddForce(repelDirection * springForce, ForceMode2D.Impulse);
            }
        }
    }
}
using UnityEngine;

namespace Game.Effects
{
    public class Projectile : MonoBehaviour
    {
        public float projectileSpeed = 5f;
        public int maxBounces = 5;
        private int _bounceCount;
        
        private Rigidbody2D _rb;
        private Vector2 _lastVelocity;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.right * projectileSpeed;
        }

        private void Update()
        {
            _lastVelocity = _rb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectDirection = Vector2.Reflect(_lastVelocity, normal);
            _rb.velocity = reflectDirection;

            _bounceCount++;
            Debug.Log("Bounce");
            
            if (_bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
}
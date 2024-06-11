using Features.Babushka_Basic.Scripts;
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public BabushkaMain babushkaMainScript;
        
        public float projectileSpeed = 5f;
        public int maxBounces = 5;
        private int _bounceCount;
        
        private Rigidbody2D _rb;
        private Vector2 _lastVelocity;
        
        private void Start()
        {
            babushkaMainScript = FindObjectOfType<BabushkaMain>();
            
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

            float angle = Mathf.Atan2(reflectDirection.y, reflectDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            _bounceCount++;
            
            if (_bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Babushkas"))
            {
                babushkaMainScript.walkingSpeed *= 0.5f;
            }
        }
    }
}

using System.Collections;
using Features.Babushka_Basic.Scripts;
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private BabushkaMain _babushkaMainScript;
        
        public float projectileSpeed = 10f;
        public int maxBounces = 5;
        private int _bounceCount;
        
        private Rigidbody2D _rb;
        private Vector2 _lastVelocity;
        private static readonly int IsPushed = Animator.StringToHash("isPushed");

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
                var babushka = other.gameObject;
                _babushkaMainScript = babushka.GetComponent<BabushkaMain>();
                
                if (_babushkaMainScript != null)
                {
                    StartCoroutine(FreezeBabushka(_babushkaMainScript, 5f));
                }
            }
        }

        private IEnumerator FreezeBabushka(BabushkaMain babushkaMainScript,float duration)
        {
            var originalWalkingSpeed = babushkaMainScript.walkingSpeed;
            RigidbodyType2D originalBodyType = babushkaMainScript._rigidbody.bodyType;
            
            //Изменение состояния
            babushkaMainScript.walkingSpeed = 0;
            babushkaMainScript._rigidbody.bodyType = RigidbodyType2D.Static;
            babushkaMainScript.animation.SetBool(IsPushed, false);
            
            yield return new WaitForSeconds(duration);
            
            //Возвращение исходного состояния
            babushkaMainScript.walkingSpeed = originalWalkingSpeed;
            babushkaMainScript._rigidbody.bodyType = originalBodyType;
            babushkaMainScript.animation.SetBool(IsPushed, true);
            babushkaMainScript.isFrozen = false;
        }
    }
}

using UnityEngine;

namespace Resources.Collectables.Scripts
{
    public class Collectables : MonoBehaviour
    {
    
        private Rigidbody2D _rigidbody;

        public bool canBeCollected;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            gameObject.layer = LayerMask.NameToLayer("No Collision");
        }
        
        private void Update()
        {
            if (transform.parent != null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            }

        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                gameObject.layer = LayerMask.NameToLayer("Collectables");
                canBeCollected = true;
            }
        }

    }
}

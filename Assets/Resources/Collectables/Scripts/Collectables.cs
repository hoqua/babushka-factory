using UnityEngine;

namespace Resources.Collectables.Scripts
{
    public class Collectables : MonoBehaviour
    {
    
        private Rigidbody2D _rigidbody;

        public bool canBeDeleted;
        
        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("No Collision");
        }
        
        private void Update()
        {
            if ( transform.parent != null) //Если бабушка является дочерним элементом (то есть схвачена клешней), блокирует движение по оси X
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else //Если не является дочерним элементом, возвращает физику 
            {
                _rigidbody = GetComponent<Rigidbody2D>();
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.CompareTag("Claw"))
            {
                _rigidbody.isKinematic = true;
                canBeDeleted = true;
            }
            else
            {
                canBeDeleted = false;
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                gameObject.layer = LayerMask.NameToLayer("Collectables");
            }
        }

    }
}

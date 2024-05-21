using UnityEngine;

namespace Game
{
    public class Collectables : MonoBehaviour
    {
    
        private Rigidbody2D collectable;
        public bool isMagnetized;
        private Vector3 targetPosition;
        public float magneticForce = 1;

        public bool canBeDeleted;

        private void Awake()
        {
            collectable = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("No Collision");
        }
        
        private void Update()
        {
            if ( transform.parent != null) //Если бабушка является дочерним элементом (то есть схвачена клешней), блокирует движение по оси X
            {
                collectable.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else //Если не является дочерним элементом, возвращает физику 
            {
                collectable = GetComponent<Rigidbody2D>();
                collectable.bodyType = RigidbodyType2D.Dynamic;
            }

        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.CompareTag("Claw"))
            {
                collectable.isKinematic = true;
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
        
        private void FixedUpdate()
        {
            if (isMagnetized)
            {
                Vector2 targetDirection = (targetPosition - transform.position).normalized;
                collectable.velocity = new Vector2(targetDirection.x, 0) * magneticForce;
            }
        }

        public void SetTarget(Vector3 position)
        {
            targetPosition = position;
            isMagnetized = true;
        }

        public void ClearTarget()
        {
            targetPosition = transform.position;
        }

    }
}

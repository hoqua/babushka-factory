using System;
using UnityEngine;

namespace Game
{
    public class Claw : MonoBehaviour
    {
        public string objectToIgnoreTag = "UI";
        
        public float clawSpeed = 5f; 
        private Vector2 initialPosition;
        private Collider2D clawCollider;
        
        private Vector2 targetPosition; // Позиция, к которой объект должен двигаться
        private MovingDirection? movingDirection = null;
        private float babushkaGrabbed;

        private void Start()
        {
            initialPosition = transform.position;
            clawCollider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0) && movingDirection == null)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                //Игнорирует нажатия на объекты с тэгом UI (если у них есть коллайдер)
                RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag(objectToIgnoreTag))
                {
                    return;
                }

                movingDirection = MovingDirection.Horizontal;
            }

            if (movingDirection == MovingDirection.Horizontal)
            {
                MoveHorizontal();
            }

            if (movingDirection == MovingDirection.Down)
            {

                MoveDown();

            }

            if (movingDirection == MovingDirection.Up)
            {
                MoveUp();
            }

        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Conveyor") || other.gameObject.CompareTag("Claw Stopper"))
            {
                movingDirection = MovingDirection.Up;
            }

            if (other.gameObject.CompareTag("Babushka"))
            {
                var babushka = other;
                babushka.transform.parent = transform;
                clawCollider.enabled = false;
                
                babushkaGrabbed= 5.5f;
                movingDirection = MovingDirection.Up;
            }
       
        }
        
        private void OnTransformChildrenChanged()
        {
            babushkaGrabbed = 0;
            movingDirection = MovingDirection.Up;
            MoveUp();
        }


        void MoveHorizontal()
        {
            var horizontalTarget = new Vector2(targetPosition.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, horizontalTarget, Time.deltaTime * clawSpeed);

            if (Math.Abs(transform.position.x - horizontalTarget.x) < 0.0001f)
            {
                movingDirection = MovingDirection.Down;
            }
        }

        void MoveDown()
        {
            var verticalTarget = new Vector2(transform.position.x, targetPosition.y - 100000);
            transform.position = Vector3.MoveTowards(transform.position, verticalTarget, Time.deltaTime * clawSpeed);

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
                movingDirection = MovingDirection.Up;
            }
        }

        void MoveUp()
        {
            var verticalTarget = new Vector2(transform.position.x, initialPosition.y + babushkaGrabbed);
            transform.position = Vector3.MoveTowards(transform.position, verticalTarget, Time.deltaTime * clawSpeed);

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
                movingDirection = null;
                clawCollider.enabled = true;
            }
        }

   
    
    }


    enum MovingDirection
    {
        Horizontal, 
        Up,
        Down
    }
}
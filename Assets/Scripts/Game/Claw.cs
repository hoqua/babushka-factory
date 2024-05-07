using System;
using UnityEngine;

namespace Game
{
    public class Claw : MonoBehaviour
    {
        
        private Vector2 initialPosition;
        public float clawSpeed = 5f;
        private Vector2 targetPosition; // Позиция, к которой объект должен двигаться
        private MovingDirection? movingDirection = null;
        private float babushkaGrabbed;

        private void Start()
        {
            initialPosition = transform.position;

        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0) && movingDirection == null)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
           
                babushkaGrabbed= 5.5f;
                movingDirection = MovingDirection.Up;
                MoveUp();
            }
       
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Babushka"))
            {
                babushkaGrabbed = 0;
                movingDirection = MovingDirection.Up;
                MoveUp();
            }
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
using System;
using UnityEngine;

namespace Game
{
    public class BabushkaMain : MonoBehaviour
    {
        public Spawner spawnerScript;
        
        private Rigidbody2D babushka;
        public bool isMagnetized;
        private Vector3 targetPosition;
        public float magneticForce = 1;
        
        public float walkingSpeed; 
        public bool canBeDeleted;
        
        public new Animator animation;
        private static readonly int IsPushed = Animator.StringToHash("isPushed");
        
        void Awake()
        {
            babushka = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (isMagnetized)
            {
                Vector2 targetDirection = (targetPosition - transform.position).normalized;
                babushka.velocity = new Vector2(targetDirection.x, 0) * magneticForce;
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
            isMagnetized = false;
        }
        
        void Start()
        {
            animation = GetComponent<Animator>();
            gameObject.layer = LayerMask.NameToLayer("No Collision");
            spawnerScript = FindObjectOfType<Spawner>();
        }

        private void OnDestroy()
        {
            spawnerScript.RemoveBabushka(this);
        }
        
        //Триггерит анимацию ходьбы пока бабушка на конвейере
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                animation.SetBool(IsPushed, true);
                gameObject.layer = LayerMask.NameToLayer("Babushkas");
                
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                transform.Translate(Vector2.left * walkingSpeed * Time.deltaTime); //Бабушка ходит влево cо скоростью walkingSpeed
            }
            
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                animation.SetBool(IsPushed, false);
            }
        }

        private void Update()
        {
            if ( transform.parent != null) //Если бабушка является дочерним элементом (то есть схвачена клешней), блокирует движение по оси X
            {
                babushka.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else //Если не является дочерним элементом, возвращает физику 
            {
                babushka = GetComponent<Rigidbody2D>();
                babushka.bodyType = RigidbodyType2D.Dynamic;
            }
           
            
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Claw"))
            {
                
                babushka.isKinematic = true; 
                animation.SetBool(IsPushed, false);
            
                canBeDeleted = true;
            }
            else
            {
                canBeDeleted = false;
            }
        
        }
    
        //Отключает анимацию пока бабушка в клешне
        private void OnCollisionStay2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Claw"))
            {
                animation.SetBool(IsPushed, false);
            }
        }

    }
}
using UnityEngine;

namespace Game
{
    public class BabushkaMain : MonoBehaviour
    {
        public Spawner spawnerScript;
        
        private Rigidbody2D _rigidbody;
        
        public float walkingSpeed; 
        public bool canBeDeleted;
        
        public new Animator animation;
        private static readonly int IsPushed = Animator.StringToHash("isPushed");
      

        void Start()
        {
            spawnerScript = FindObjectOfType<Spawner>();
            
            _rigidbody = GetComponent<Rigidbody2D>();
            gameObject.layer = LayerMask.NameToLayer("No Collision");
            
            animation = GetComponent<Animator>();
        }
        
        private void OnDestroy()
        {
            spawnerScript.RemoveBabushka(this);
        }

        void Update()
        {
            _rigidbody.bodyType = transform.parent == null ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
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
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Claw") && transform.parent == null) 
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.isKinematic = true; 
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
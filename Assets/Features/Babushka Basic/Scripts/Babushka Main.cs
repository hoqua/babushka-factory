using UnityEngine;

namespace Features.Babushka_Basic.Scripts
{
    public class BabushkaMain : MonoBehaviour
    {
        public BabushkaSoundController babushkaSoundController;
        
        public Rigidbody2D _rigidbody;
        
        public float walkingSpeed; 
        public bool canBeCollected;
        
        public new Animator animation;
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsPushed = Animator.StringToHash("isPushed");
        private static readonly int IsGrabbed = Animator.StringToHash("isGrabbed");


        void Start()
        {
            babushkaSoundController = FindObjectOfType<BabushkaSoundController>();
            
            _rigidbody = GetComponent<Rigidbody2D>();
            gameObject.layer = LayerMask.NameToLayer("No Collision");

            animation = GetComponent<Animator>();
        }

        void Update()
        {
            animation.SetBool(IsFalling, _rigidbody.velocity.y < -1f); //Анимация падения
        }
        
        //Триггерит анимацию ходьбы пока бабушка на конвейере, а также делает их "collectable"
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                babushkaSoundController.IsFellSfx(); //Воспроизводит звук падения
                
                animation.Play("Babushka Walking");
                animation.SetBool(IsPushed, true);
                gameObject.layer = LayerMask.NameToLayer("Babushkas");

                canBeCollected = true;
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                transform.Translate(Vector2.left * walkingSpeed * Time.deltaTime); //Бабушка ходит влево cо скоростью walkingSpeed
            }
            
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Claw") && transform.parent == null) 
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.isKinematic = true;

                animation.Play("Babushka_Grabbed");
                animation.SetBool(IsPushed, false);
                animation.SetBool(IsGrabbed, true);
                
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

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Claw"))
            {
               _rigidbody.constraints = RigidbodyConstraints2D.None;
               _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
               _rigidbody.isKinematic = false;
               
               animation.Play("Babushka Walking");
               animation.SetBool(IsPushed, true);
               animation.SetBool(IsGrabbed, false);

            }
            
        }

    }
}
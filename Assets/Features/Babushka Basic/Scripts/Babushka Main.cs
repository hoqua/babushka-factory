using System;
using UnityEngine;

namespace Features.Babushka_Basic.Scripts
{
    public class BabushkaMain : MonoBehaviour
    {
        public BabushkaSoundController babushkaSoundController;
        
        public Rigidbody2D _rigidbody;
        
        public float walkingSpeed;
        private const float MinWalkingSpeed = 0.75f;
        private const float MaxWalkingSpeed = 1.25f;
        
        public bool canBeCollected;
        public bool isFrozen = false;
        
        public new Animator animation;
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsPushed = Animator.StringToHash("isPushed");
        private static readonly int IsGrabbed = Animator.StringToHash("isGrabbed");
        private static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");


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

            var clampedWalkingSpeed = Mathf.Clamp(walkingSpeed, MinWalkingSpeed, MaxWalkingSpeed);
            animation.SetFloat(WalkSpeed, clampedWalkingSpeed);

            if (transform.parent != null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;

                animation.Play("Babushka_Grabbed");
                animation.SetBool(IsPushed, false);
                animation.SetBool(IsGrabbed, true);
            }
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
            
            if (other.CompareTag("Claw")) 
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;

                animation.Play("Babushka_Grabbed");
                animation.SetBool(IsPushed, false);
                animation.SetBool(IsGrabbed, true);
                
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Conveyor"))
            {
                transform.Translate(Vector2.left * walkingSpeed * Time.deltaTime); //Бабушка ходит влево cо скоростью walkingSpeed
            }

            if (other.CompareTag("Claw"))
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;

                animation.Play("Babushka_Grabbed");
                animation.SetBool(IsPushed, false);
                animation.SetBool(IsGrabbed, true);
            }
            
        }
        
        
        

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Claw"))
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                
                animation.Play("Babushka Walking");
                animation.SetBool(IsPushed, true);
                animation.SetBool(IsGrabbed, false);
            }
            
        }

    }
}
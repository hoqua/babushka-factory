using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Features.Claw.Scripts
{
    public class Claw : MonoBehaviour
    {
        public PlayerManager playerManager;
        public MagnetController magnetController;
        
        public string objectToIgnoreTag = "UI";
        public bool isInputBlocked = false;
        
        
        public float clawSpeed = 5f;
        public int maxGrabbedBabushkas = 1;
        
        private Vector2 _initialPosition;
        public BoxCollider2D clawCollider;
        public Transform clawObject;
        
        private Vector2 _targetPosition; // Позиция, к которой объект должен двигаться
        internal MovingDirection? _movingDirection;
        
        private float _isObjectGrabbed;
        private readonly List<GameObject> _grabbedBabushkas = new List<GameObject>();

        public AudioClip clawSound;
        private AudioSource _audioSource;
        public bool isClawSoundPlaying;

        private void Start()
        {
            _initialPosition = transform.position;
            _audioSource = GetComponent<AudioSource>();
        }
        
        void Update()
        {
            if (isInputBlocked) return;
            
            if (Input.GetMouseButtonDown(0) && _movingDirection == null)
            {
                if (Camera.main != null) _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Игнорирует нажатия на объекты с тэгом UI (если у них есть коллайдер)
                RaycastHit2D hit = Physics2D.Raycast(_targetPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag(objectToIgnoreTag))
                {
                    return;
                }
                
                _movingDirection = MovingDirection.Horizontal;
            }
            
            if (_movingDirection == MovingDirection.Horizontal)
            {
                
                MoveHorizontal();
            }

            if (_movingDirection == MovingDirection.Down)
            {

                MoveDown();

            }

            if (_movingDirection == MovingDirection.Up)
            {
                MoveUp();
            }
            
        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Conveyor"))
            {
                magnetController.ActivateMagnet();
                _movingDirection = MovingDirection.Up;
                playerManager.CheckDurability();
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            
            if (other.CompareTag("Collectable"))
            {
                
                var collectable = other.gameObject;
                collectable.transform.parent = transform;
                   
                _isObjectGrabbed= 5.5f;  //Добавляет дополнительное расстояние к цели клешни, чтобы она двигалась вверх
                _movingDirection = MovingDirection.Up;
                
            }

            else if (other.CompareTag("Babushka"))
            {
                
                if (_grabbedBabushkas.Count >= maxGrabbedBabushkas) return; 
                
                var babushka = other.gameObject;
                if (!babushka.transform.IsChildOf(transform) && _grabbedBabushkas.Count < maxGrabbedBabushkas) 
                {
                    
                    if (!_grabbedBabushkas.Contains(babushka)) 
                    {
                        babushka.transform.parent = transform;
                        _grabbedBabushkas.Add(babushka);
                    }
                    
                    _isObjectGrabbed= 5.5f;  //Добавляет дополнительное расстояние к цели клешни, чтобы она двигалась вверх
                    _movingDirection = MovingDirection.Up;
                }
                
            }

            if (other.CompareTag("Babushka") || other.CompareTag("Collectable"))
            {
                magnetController.ActivateMagnet();
            }
            
        }

        private void OnTransformChildrenChanged()
        {
            _grabbedBabushkas.RemoveAll(obj => obj == null);
            _grabbedBabushkas.Clear();
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Babushka") && !_grabbedBabushkas.Contains(child.gameObject))
                {
                    _grabbedBabushkas.Add(child.gameObject);
                }
            }

            if (_grabbedBabushkas.Count == 0)
            {
                _isObjectGrabbed = 0;
                _movingDirection = MovingDirection.Up;
                MoveUp();
            }
        }


        void MoveHorizontal()
        {
            if (!isClawSoundPlaying) PlayClawSound();

            var clawPosition = transform.position;
            var horizontalTarget = new Vector2(_targetPosition.x, clawPosition.y);
            clawPosition = Vector2.MoveTowards(clawPosition, horizontalTarget, Time.deltaTime * clawSpeed);
            transform.position = clawPosition;

            if (Math.Abs(transform.position.x - horizontalTarget.x) < 0.0001f)
            {
                _movingDirection = MovingDirection.Down;
            }
        }

        void MoveDown()
        {
            var clawPosition = transform.position;
            var verticalTarget = new Vector2(clawPosition.x, _targetPosition.y - 100000);
            clawPosition = Vector3.MoveTowards(clawPosition, verticalTarget, Time.deltaTime * clawSpeed);
            transform.position = clawPosition;

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
                _movingDirection = MovingDirection.Up;
            }
        }

        void MoveUp()
        {
            var clawPosition = transform.position;
            var verticalTarget = new Vector2(clawPosition.x, _initialPosition.y + _isObjectGrabbed);
            clawPosition = Vector3.MoveTowards(clawPosition, verticalTarget, Time.deltaTime * clawSpeed);
            transform.position = clawPosition;

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
                _movingDirection = null;
                StopClawSound();
            }
        }

        public void PlayClawSound()
        {
            if (!isInputBlocked)
            {
                _audioSource.Stop();
                _audioSource.PlayOneShot(clawSound);
                _audioSource.loop = true;
                isClawSoundPlaying = true;
            }
        }

        public void StopClawSound()
        {
            _audioSource.Stop();
            isClawSoundPlaying = false;
        }
    }


    enum MovingDirection
    {
        Horizontal, 
        Up,
        Down
    }
}
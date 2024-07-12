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
        
        private Vector2 _initialPosition;
        public BoxCollider2D clawGrabTrigger;
        public Transform clawTransform;
        
        private Vector2 _targetPosition; // Позиция, к которой объект должен двигаться
        internal MovingDirection? _movingDirection;
        
        private float _isObjectGrabbed;
        private readonly List<GameObject> _grabbedBabushkas = new List<GameObject>();

        private ClawAudioController _clawAudioController;

        private void Start()
        {
            _initialPosition = transform.position;
            _clawAudioController = GetComponent<ClawAudioController>();
            clawGrabTrigger.enabled = false;
        }
        
        void Update()
        {
            if (isInputBlocked)
            {
                _clawAudioController.StopClawSound();
                return;
            }
            
            if (_movingDirection == null)
            {
                magnetController.magnetCollider.enabled = false;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                
                
                if (_movingDirection == null)
                {
                    clawGrabTrigger.enabled = false;
                }
                
                if (_movingDirection != null)
                {
                    _movingDirection = MovingDirection.ReturningUp;
                }
                else
                {
                    if (Camera.main != null) _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Raycast(_targetPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.CompareTag(objectToIgnoreTag))
                    {
                        return;
                    }
                    
                    _movingDirection = MovingDirection.Horizontal;
                }

                
            }

            switch (_movingDirection)
            {
                case MovingDirection.Horizontal:
                    MoveHorizontal();
                    break;
                case MovingDirection.Down:
                    MoveDown();
                    break;
                case MovingDirection.Up:
                    MoveUp();
                    break;
                case MovingDirection.ReturningUp:
                    ReturnUp();
                    break;
                case MovingDirection.ReturningHorizontal:
                    ReturnHorizontal();
                    break;
            }
        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (other.gameObject.CompareTag("Conveyor"))
            {
                magnetController.ActivateMagnet();
                _movingDirection = MovingDirection.Up;
                playerManager.CheckDurability();
                clawGrabTrigger.enabled = true;
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            
            if (other.CompareTag("Collectable") || other.CompareTag("Babushka"))
            {
                
                var collectable = other.gameObject;
                collectable.transform.parent = transform;
                   
                _isObjectGrabbed= 6f;  //Добавляет дополнительное расстояние к цели клешни, чтобы она двигалась вверх
                _movingDirection = MovingDirection.Up;
                
                magnetController.ActivateMagnet();
            }
            
        }

        private void OnTransformChildrenChanged()
        {
            _isObjectGrabbed = 0f;
            _movingDirection = MovingDirection.Up;
        }
        
        bool HasChildWithTag(string tag)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }

        void MoveHorizontal()
        {
            if (!_clawAudioController.isClawSoundPlaying) _clawAudioController.PlayClawSound();

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
                if (HasChildWithTag("Babushka") || HasChildWithTag("Collectable"))
                {
                    _isObjectGrabbed = 6f;
                    MoveUp();
                }
                else
                {
                    _movingDirection = null;
                    _initialPosition = transform.position;
                    _clawAudioController.StopClawSound();
                }
                
                
            }
        }

        void ReturnUp()
        {
            clawGrabTrigger.enabled = true;
            
            var clawPosition = transform.position;
            var verticalTarget = new Vector2(clawPosition.x, _initialPosition.y + _isObjectGrabbed);
            clawPosition = Vector3.MoveTowards(clawPosition, verticalTarget, Time.deltaTime * clawSpeed);
            transform.position = clawPosition;

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
                clawGrabTrigger.enabled = false;
                _movingDirection = MovingDirection.ReturningHorizontal;
            }
        }

        void ReturnHorizontal()
        {
            if (!_clawAudioController.isClawSoundPlaying) _clawAudioController.PlayClawSound();

            var clawPosition = transform.position;
            var horizontalTarget = _initialPosition;
            clawPosition = Vector2.MoveTowards(clawPosition, horizontalTarget, Time.deltaTime * clawSpeed);
            transform.position = clawPosition;

            if (Math.Abs(transform.position.x - horizontalTarget.x) < 0.0001f)
            {
                _movingDirection = null;
                _clawAudioController.StopClawSound();
            }
        }
    }

    enum MovingDirection
    {
        Horizontal, 
        Up,
        Down,
        ReturningUp,
        ReturningHorizontal
    }
}

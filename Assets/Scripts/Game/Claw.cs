using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Claw : MonoBehaviour
    {
        
        public string objectToIgnoreTag = "UI";
        public PlayerManager playerManager; 
        
        public float clawSpeed = 5f;
        public int maxGrabbedBabushkas = 1;
        
        private Vector2 initialPosition;
        public  BoxCollider2D clawCollider;
        public GameObject magnetCollider;
        public Transform clawObject;
        
        private Vector2 targetPosition; // Позиция, к которой объект должен двигаться
        private MovingDirection? movingDirection = null;
        
        private float ifObjectGrabbed;
        private List<GameObject> grabbedBabushkas = new List<GameObject>();

        public ulong clawSound;
        private AudioSource _audioSource;
        public bool isClawSoundPlaying = false;
        
        private void Start()
        {
            initialPosition = transform.position;
            _audioSource = GetComponent<AudioSource>();
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
                
                playerManager.CheckDurability();
                movingDirection = MovingDirection.Horizontal;
            }

            magnetCollider.SetActive(movingDirection != null);

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
             
                
            if (other.gameObject.CompareTag("Conveyor"))
            {
                movingDirection = MovingDirection.Up;
            }

            if (other.gameObject.CompareTag("Collectable"))
            {
                
                var collectable = other.gameObject;
                collectable.transform.parent = transform;
                
                clawCollider.enabled = false;
                
                ifObjectGrabbed= 5.5f;  //Добавляет дополнительное расстояние к цели клешни, чтобы она двигалась вверх
                movingDirection = MovingDirection.Up;
                
            }

            else if (other.gameObject.CompareTag("Babushka"))
            {
                if (grabbedBabushkas.Count >= maxGrabbedBabushkas) return; 
                
                var babushka = other.gameObject;
                if (!babushka.transform.IsChildOf(transform) && grabbedBabushkas.Count < maxGrabbedBabushkas) 
                {
                    
                    if (!grabbedBabushkas.Contains(babushka)) 
                    {
                        babushka.transform.parent = transform;
                        grabbedBabushkas.Add(babushka);
                    }
                    clawCollider.enabled = false;
                    
                    ifObjectGrabbed= 5.5f;  //Добавляет дополнительное расстояние к цели клешни, чтобы она двигалась вверх
                    movingDirection = MovingDirection.Up;
                }
                
            }
       
        }
        
        private void OnTransformChildrenChanged()
        {
            grabbedBabushkas.RemoveAll(obj => obj == null);
            grabbedBabushkas.Clear();
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Babushka") && !grabbedBabushkas.Contains(child.gameObject))
                {
                    grabbedBabushkas.Add(child.gameObject);
                }
            }

            if (grabbedBabushkas.Count == 0)
            {
                ifObjectGrabbed = 0;
                movingDirection = MovingDirection.Up;
                MoveUp();
            }
        }


        void MoveHorizontal()
        {
            if (!isClawSoundPlaying) PlayClawSound();
            
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
            var verticalTarget = new Vector2(transform.position.x, initialPosition.y + ifObjectGrabbed);
            transform.position = Vector3.MoveTowards(transform.position, verticalTarget, Time.deltaTime * clawSpeed);

            if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
            {
          
                movingDirection = null;
                clawCollider.enabled = true;
                StopClawSound();
            }
        }

        public void PlayClawSound()
        {
            _audioSource.loop = true;
            _audioSource.Play(clawSound);
            isClawSoundPlaying = true;

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
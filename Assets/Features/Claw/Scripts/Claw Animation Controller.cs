using System;
using UnityEngine;

namespace Features.Claw.Scripts
{
    public class ClawAnimationController : MonoBehaviour
    {
        public Claw clawScript;
        
        private Animator _animation;
        private static readonly int IsGrabbing = Animator.StringToHash("isGrabbing");

        private void Start()
        {
            _animation = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Conveyor"))
            {
                _animation.SetBool(IsGrabbing, true);
            }
        }

        private void Update()
        {
            if (clawScript._movingDirection == null)
            {
                _animation.SetBool(IsGrabbing, false);
            }
        }
    }
}
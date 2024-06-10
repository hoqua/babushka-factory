using Game;
using UnityEngine;

namespace Features.Babushka_Basic.Scripts
{
    public class BabushkaSoundController : MonoBehaviour
    {
        public BabushkaMain babushkaMainScript;
        
        private AudioSource _audioSource;
        public AudioClip footstepClip;
       
        public float stepInterval = 1f;
        private float _stepTimer;

        private void Start()
        {
            babushkaMainScript = FindObjectOfType<BabushkaMain>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = footstepClip;
            _audioSource.volume = 0.2f; 
        }

        private void Update()
        {
            if (IsWalking())
            {
                _stepTimer -= Time.deltaTime;
                if (_stepTimer <= 0)
                {
                    PlayFootstep();
                    _stepTimer = stepInterval;
                }
            }
            else
            {
                _stepTimer = 0;
            }
        }

        private bool IsWalking()
        {
            return babushkaMainScript._rigidbody.velocity.x > 0.1f;
        }
        
        private void PlayFootstep()
        {
            _audioSource.PlayOneShot(footstepClip);
        }

    }
}

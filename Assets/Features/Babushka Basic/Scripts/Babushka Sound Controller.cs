using Game;
using UnityEngine;

namespace Features.Babushka_Basic.Scripts
{
    public class BabushkaSoundController : MonoBehaviour
    {
        public BabushkaMain babushkaMainScript;
        
        private AudioSource _audioSource;
        public AudioClip footstepClip;
        public AudioClip[] fallClips; 

        public float footstepVolume = 0.75f;
        public float fallVolume = 0.3f;

        private const float MinStepInterval = 0.1f;
        private const float MaxStepInterval = 1f;
        
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
            if (IsWalkingSfx())
            {
                float stepInterval = Mathf.Clamp(1f / babushkaMainScript.walkingSpeed, MinStepInterval, MaxStepInterval);
                
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

        private bool IsWalkingSfx()
        {
            return babushkaMainScript._rigidbody.velocity.x > 0.1f;
        }

        public void IsFellSfx()
        {
            if (fallClips.Length > 0)
            {
                int randomIndex = Random.Range(0, fallClips.Length);
                AudioClip fallClip = fallClips[randomIndex]; 
                _audioSource.PlayOneShot(fallClip, fallVolume);
            }
            else
            {
                Debug.LogWarning("No fall clips assigned!");
            }
        }
        
        private void PlayFootstep()
        {
            _audioSource.PlayOneShot(footstepClip, footstepVolume);
        }
    }
}

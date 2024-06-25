using Game;
using UnityEngine;

namespace Features.Babushka_Basic.Scripts
{
    public class BabushkaSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip[] fallClips; 
        
        public float fallVolume = 0.3f;
        
        private float _stepTimer;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.5f; 
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
        
    }
}

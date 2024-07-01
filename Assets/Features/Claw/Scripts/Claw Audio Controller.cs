using UnityEngine;

namespace Features.Claw.Scripts
{
    public class ClawAudioController : MonoBehaviour
    {
        public AudioClip clawSound;
        private AudioSource _audioSource;
        public bool isClawSoundPlaying;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayClawSound()
        {
            if (!_audioSource.isPlaying)
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
}
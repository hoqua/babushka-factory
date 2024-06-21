using Features.Babushka_Basic.Scripts;
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class ProjectileSoundController : MonoBehaviour
    {
        private BabushkaMain _babushkaMainScript;
        
        private AudioSource _audioSource;
        public AudioClip freezeSound;

        public float freezeSoundVolume = 0.15f;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.5f;
        }
        
        public void PlayFreezeSound()
        {
            _audioSource.PlayOneShot(freezeSound, freezeSoundVolume);
        }
    }
}

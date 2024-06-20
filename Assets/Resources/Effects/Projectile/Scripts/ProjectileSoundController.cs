using Features.Babushka_Basic.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class ProjectileSoundController : MonoBehaviour
    {
        private BabushkaMain _babushkaMainScript;
        
        private AudioSource _audioSource;
        public AudioClip freezeSound;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.15f;
        }
        
        public void PlayFreezeSound()
        {
            _audioSource.PlayOneShot(freezeSound);
        }
    }
}

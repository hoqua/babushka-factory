
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class ProjectileSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip freezeSound;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.25f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Babushkas"))
            {
                _audioSource.PlayOneShot(freezeSound);
            }
        }
    }
}

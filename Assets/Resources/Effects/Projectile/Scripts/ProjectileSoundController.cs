
using Features.Babushka_Basic.Scripts;
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
            _audioSource.volume = 0.25f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Babushkas"))
            {
                var babushka = other.gameObject;
                _babushkaMainScript = babushka.GetComponent<BabushkaMain>();
                
                if (_babushkaMainScript.isFrozen == false)
                {
                    _audioSource.PlayOneShot(freezeSound);
                    _babushkaMainScript.isFrozen = true;
                }
                
            }
        }
    }
}

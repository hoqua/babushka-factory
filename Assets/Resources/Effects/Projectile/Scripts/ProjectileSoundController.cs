
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class ProjectileSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip freezeSound;

        private readonly string _targetLayerName = "Babushkas";
        private int _targetLayer;

        private void Start()
        {
            _targetLayer = LayerMask.NameToLayer(_targetLayerName);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.25f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == _targetLayer)
            {
                _audioSource.PlayOneShot(freezeSound);
            }
        }
    }
}

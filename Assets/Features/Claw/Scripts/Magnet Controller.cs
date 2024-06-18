using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Claw.Scripts
{
    public class MagnetController : MonoBehaviour
    {
        private PointEffector2D _magnetEffect;
        private CircleCollider2D _magnetCollider;

        public float magnetActiveTime = 0.5f;
        
        void Start()
        {
            _magnetEffect = FindObjectOfType<PointEffector2D>();
            _magnetCollider = FindObjectOfType<CircleCollider2D>();
           
            _magnetCollider.enabled = false;
            _magnetEffect.enabled = false;
        }
        
        public void EnableMagnet()
        {
            _magnetCollider.enabled = false;
            _magnetEffect.enabled = true;
        }

        public void ActivateMagnet()
        {
            StartCoroutine(MagnetCoroutine(magnetActiveTime));
        }
        
       private IEnumerator MagnetCoroutine(float duration)
        {
            _magnetCollider.enabled = true;
            yield return new WaitForSeconds(duration);
            _magnetCollider.enabled = false;
        }
        
    }
}
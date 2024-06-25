using System.Collections;
using UnityEngine;


namespace Features.Claw.Scripts
{
    public class MagnetController : MonoBehaviour
    {
        public PointEffector2D magnetEffect;
        private CircleCollider2D _magnetCollider;

        public float magnetActiveTime = 0.5f;
        
        void Start()
        {
            magnetEffect = FindObjectOfType<PointEffector2D>();
            _magnetCollider = FindObjectOfType<CircleCollider2D>();
           
            _magnetCollider.enabled = false;
            magnetEffect.enabled = false;
        }
        
        public void EnableMagnet()
        {
            _magnetCollider.enabled = false;
            magnetEffect.enabled = true;
        }

        public void UpgradeMagnet()
        {
            _magnetCollider.radius += 0.1f;
            magnetEffect.forceMagnitude -= 5f;
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
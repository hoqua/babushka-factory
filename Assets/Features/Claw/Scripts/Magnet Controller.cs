using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


namespace Features.Claw.Scripts
{
    public class MagnetController : MonoBehaviour
    {
        public PointEffector2D magnetEffect;
        public CircleCollider2D magnetCollider;

        public float magnetActiveTime = 0.5f;
        
        void Start()
        {
            magnetEffect = FindObjectOfType<PointEffector2D>();
            magnetCollider = FindObjectOfType<CircleCollider2D>();
           
            magnetCollider.enabled = false;
            magnetEffect.enabled = false;
        }
        
        public void EnableMagnet()
        {
            magnetCollider.enabled = false;
            magnetEffect.enabled = true;
        }

        public void UpgradeMagnet()
        {
            magnetCollider.radius += 0.1f;
            magnetEffect.forceMagnitude -= 5f;
        }

        public void ActivateMagnet()
        {
            StartCoroutine(MagnetCoroutine(magnetActiveTime));
        }
        
       private IEnumerator MagnetCoroutine(float duration)
        {
            magnetCollider.enabled = true;
            yield return new WaitForSeconds(duration);
            magnetCollider.enabled = false;
        }
        
    }
}
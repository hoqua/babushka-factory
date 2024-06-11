using UnityEngine;

namespace Features.Claw.Scripts
{
    public class MagnetController : MonoBehaviour
    {
        private PointEffector2D _magnetEffect;
        
        void Start()
        {
            _magnetEffect = FindObjectOfType<PointEffector2D>();
            _magnetEffect.enabled = false;
        }

        public void ActivateMagnet()
        {
            _magnetEffect.enabled = true;
        }
    }
}
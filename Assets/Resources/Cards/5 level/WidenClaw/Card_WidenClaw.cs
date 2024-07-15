using UnityEngine;

namespace Resources.Cards._1_level.WidenClaw
{
    public class CardWidenClaw : MonoBehaviour
    {
        private Transform _clawTransform;
        private readonly Vector3 _biggerClaw = new(0.05f, 0.05f, 0);
        private void Awake()
        {
            GameObject claw = GameObject.Find("Claw");
            _clawTransform = claw.transform;
        }

        private void OnMouseDown()
        {
            _clawTransform.localScale += _biggerClaw;
        }
    }
}

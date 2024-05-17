using System;
using UnityEngine;

namespace Game
{
    public class ClawMagnet : MonoBehaviour
    {
        
        private void OnTriggerStay2D(Collider2D collision) 
        {
            if (collision.gameObject.TryGetComponent<BabushkaMain>(out BabushkaMain babushka))
            {
                babushka.SetTarget(transform.parent.position);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<BabushkaMain>(out BabushkaMain babushka))
            {
                babushka.ClearTarget();
            }
        }
    }
}

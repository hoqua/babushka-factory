using System;
using UnityEngine;

namespace Game
{
    public class ClawMagnet : MonoBehaviour
    {
        
        private void OnTriggerStay2D(Collider2D collision) 
        {
            if (collision.gameObject.TryGetComponent<BabushkaMain>(out var babushka))
            {
                babushka.SetTarget(transform.parent.position);
            }

            if (collision.gameObject.TryGetComponent<Collectables>(out var collectable))
            {
                collectable.SetTarget(transform.parent.position);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<BabushkaMain>(out var babushka))
            {
                babushka.ClearTarget();
            }
            
            if (collision.gameObject.TryGetComponent<Collectables>(out var collectable))
            {
                collectable.ClearTarget();
            }
        }
    }
}

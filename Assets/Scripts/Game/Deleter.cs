using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Deleter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Babushka"))
            {
                Destroy(other.GameObject());
            }
        }
    }
}

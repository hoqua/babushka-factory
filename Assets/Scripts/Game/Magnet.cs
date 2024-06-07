using UnityEngine;

namespace Game
{
    public class Magnet : MonoBehaviour
    {
        public float magneticForce = 10f;
        public float magneticRange = 5f;

        void FixedUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, magneticRange);
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null && rb.gameObject != gameObject)
                {
                    Vector3 direction = transform.position - rb.transform.position;
                    rb.AddForce(direction.normalized * magneticForce * Time.fixedDeltaTime);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, magneticRange);
        }
    }
}
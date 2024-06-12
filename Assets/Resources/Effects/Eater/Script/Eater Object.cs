using System;
using UnityEngine;

namespace Resources.Effects.Eater.Script
{
    public class EaterObject : MonoBehaviour
    {
        public float moveSpeed;
        public float destroyTime;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        }

        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Babushka"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}

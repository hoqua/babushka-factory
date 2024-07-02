using System;
using Game.Level;
using UnityEngine;

namespace Resources.Effects.Eater.Script
{
    public class EaterObject : MonoBehaviour
    {
        public Counter counterScript;
        public Deleter deleterScript;
        public CollectablesSpawner collectablesSpawnerScript;
        
        public float moveSpeed;
        public float destroyTime;

        private void Start()
        {
            counterScript = FindObjectOfType<Counter>();
            deleterScript = FindObjectOfType<Deleter>();
            collectablesSpawnerScript = FindObjectOfType<CollectablesSpawner>();
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
                
                counterScript.collectedBabushkasCount++;
                counterScript.counterText.text = "Собрано Бабушек " + counterScript.collectedBabushkasCount;
                    
                deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / collectablesSpawnerScript.spawnedBabushkas) * 100f);
                deleterScript.deletedCounterText.text = "Упущено бабушек " + deleterScript.deletedBabushkasRatio + "%";
            }
        }
    }
}

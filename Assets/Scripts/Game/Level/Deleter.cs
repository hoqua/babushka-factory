using TMPro;
using UnityEngine;

namespace Game.Level
{
    public class Deleter : MonoBehaviour
    {
        public Counter counterScript;
        public CollectablesSpawner collectablesSpawnerScript;
        
        public TextMeshProUGUI deletedCounterText;
        public float deletedBabushkasCount;
        public float deletedBabushkasRatio;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Babushka"))
            {
                Destroy(other.gameObject);
                
                deletedBabushkasCount += 1;
                
                deletedBabushkasRatio = (int)((deletedBabushkasCount / collectablesSpawnerScript.spawnedBabushkas) * 100f);
                deletedCounterText.text = "Упущено бабушек " + deletedBabushkasRatio + "%"; 
            }

            if (other.CompareTag("Collectable"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}

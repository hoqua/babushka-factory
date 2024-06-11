using TMPro;
using UnityEngine;

namespace Game.Level
{
    public class Deleter : MonoBehaviour
    {
        public Counter counterScript;
        public Spawner spawnerScript;
        
        public TextMeshProUGUI deletedCounterText;
        public float deletedBabushkasCount;
        public float deletedBabushkasRatio;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Babushka"))
            {
                Destroy(other.gameObject);
                
                deletedBabushkasCount += 1;
                deletedBabushkasRatio = (int)((deletedBabushkasCount / counterScript.currentNumOfBabushkas) * 100f);
                if (counterScript.currentNumOfBabushkas == 0) return;
                deletedCounterText.text = "Упущено бабушек " + deletedBabushkasRatio + "%"; 
            }

            if (other.CompareTag("Collectable"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Deleter : MonoBehaviour
    {
        public Counter counterScript;
        
        public TextMeshProUGUI deletedCounterText;
        public float deletedBabushkasCount = 0;
        public float deletedBabushkasRatio;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Babushka"))
            {
                Destroy(other.GameObject());
                deletedBabushkasCount += 1;
                deletedBabushkasRatio = (int)((deletedBabushkasCount / counterScript.currentNumOfBabushkas) * 100f);
                deletedCounterText.text = "Бабушек было упущено " + deletedBabushkasRatio + "%"; 
            }
        }
    }
}

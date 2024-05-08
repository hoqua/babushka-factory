using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace Game
{
    public class Counter : MonoBehaviour
    {
        private BabushkaMain babushkaMain;
        public Player playerManager;

        public TMP_Text counterText;
        private int currentNumOfBabushkas = 0;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            babushkaMain = other.GetComponent<BabushkaMain>();
            if (other.CompareTag("Babushka") && babushkaMain.canBeDeleted)
            {
                Destroy(other.GameObject());
                playerManager.GainExp();
            
                currentNumOfBabushkas += 1;
                counterText.text = "Собрано Бабушек " + currentNumOfBabushkas.ToString();
            }
        }
        
    }
}

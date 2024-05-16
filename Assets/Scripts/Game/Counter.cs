using System;
using Game.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace Game
{
    public class Counter : MonoBehaviour
    {
        public Deleter deleterScript;
        
        private BabushkaMain babushkaMain;
        public Player playerManager;

        public TextMeshProUGUI counterText;
        public int currentNumOfBabushkas = 0;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            babushkaMain = other.GetComponent<BabushkaMain>();
            if (other.CompareTag("Babushka") && babushkaMain.canBeDeleted)
            {
                Destroy(other.GameObject());
                playerManager.GainExp();
            
                currentNumOfBabushkas += 1;
                counterText.text = "Собрано Бабушек " + currentNumOfBabushkas.ToString();
                
                deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / currentNumOfBabushkas) * 100f);
                deleterScript.deletedCounterText.text = "Упущено бабушек " + deleterScript.deletedBabushkasRatio + "%"; 
            }
        }
        
    }
}

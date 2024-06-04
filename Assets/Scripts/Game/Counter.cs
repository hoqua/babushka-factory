using Game.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Counter : MonoBehaviour
    {
        public Deleter deleterScript;
        
        private Collectables collectablesScript;
        private BabushkaMain babushkaMain;
        public PlayerManager playerManager;

        public TextMeshProUGUI counterText;
        public int currentNumOfBabushkas;

        private void Start()
        {
            collectablesScript = FindObjectOfType<Collectables>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            babushkaMain = other.GetComponent<BabushkaMain>();
            if (other.CompareTag("Babushka") && babushkaMain.canBeDeleted)
            {
                Destroy(other.GameObject());
                playerManager.GainExp();
            
                currentNumOfBabushkas += 1;
                counterText.text = "Собрано Бабушек " + currentNumOfBabushkas;
                
                deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / currentNumOfBabushkas) * 100f);
                if (currentNumOfBabushkas == 0) return;
                deleterScript.deletedCounterText.text = "Упущено бабушек " + deleterScript.deletedBabushkasRatio + "%";
                return;
            }

            collectablesScript = other.GetComponent<Collectables>();
            if (other.CompareTag("Collectable") && collectablesScript.canBeDeleted)
            {
                if (other.name == "RepairTool")
                {
                    playerManager.clawDurability = 100;
                    playerManager.clawDurabilityText.text = "Прочность клешни " + playerManager.clawDurability + "%";
                }

                if (other.name == "CookieBox")
                {
                    playerManager.currentExp = playerManager.requiredExp;
                    playerManager.CheckLevelUp();
                }

                Destroy(other.GameObject());
                
            }
        }
        
    }
}

using Features.Babushka_Basic.Scripts;
using Game.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Counter : MonoBehaviour
    {
        public Deleter deleterScript;
        
        private Collectables _collectablesScript;
        private BabushkaMain _babushkaMain;
        public PlayerManager playerManager;

        public TextMeshProUGUI counterText;
        public int currentNumOfBabushkas;

        private void Start()
        {
            _collectablesScript = FindObjectOfType<Collectables>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            _babushkaMain = other.GetComponent<BabushkaMain>();
            if (other.CompareTag("Babushka") && _babushkaMain.canBeCollected)
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

            _collectablesScript = other.GetComponent<Collectables>();
            if (other.CompareTag("Collectable") && _collectablesScript.canBeDeleted)
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

using Features.Babushka_Basic.Scripts;
using Resources.Collectables.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Level
{
    public class Counter : MonoBehaviour
    {
        public Deleter deleterScript;
        
        private Collectables _collectablesScript;
        private BabushkaMain _babushkaMain;
        public PlayerManager playerManager;
        public CollectablesSpawner collectablesSpawnerScript;

        public TextMeshProUGUI counterText;
        public int collectedBabushkasCount;
       

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
            
                collectedBabushkasCount += 1;
                counterText.text = "Собрано Бабушек " + collectedBabushkasCount;
                
                deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / collectablesSpawnerScript.spawnedBabushkas) * 100f);
                deleterScript.deletedCounterText.text = "Упущено бабушек " + deleterScript.deletedBabushkasRatio + "%";
                return;
            }

            _collectablesScript = other.GetComponent<Collectables>();
            if (other.CompareTag("Collectable") && _collectablesScript.canBeCollected)
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

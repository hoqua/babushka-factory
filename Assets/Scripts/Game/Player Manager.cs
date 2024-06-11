using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class PlayerManager : MonoBehaviour
    {
        public GameManager gameManager;
    
        public int playerLevel = 1;
        public int currentExp;
        public int requiredExp = 5;
        public int clawDurability = 100;

        public TextMeshProUGUI playerLevelText;
        public TextMeshProUGUI clawDurabilityText;
        public void GainExp()
        {
            currentExp++;
        
            CheckLevelUp();
        }

        public void CheckLevelUp()
        {
            if (currentExp >= requiredExp)
            {
                currentExp -= requiredExp;
                requiredExp = (int)(requiredExp * 1.5f);
            
                playerLevel++;
                playerLevelText.text = "Уровень " + playerLevel;

                gameManager.PauseGame();
                gameManager.ShowUpgradeOverlay();
            }
        }

        public void CheckDurability()
        {
            if (clawDurability <= 0)
            {
                gameManager.PauseGame();
                Debug.Log("Вы проиграли :(");
            }

            clawDurability--;
            clawDurabilityText.text = "Прочность клешни " + clawDurability + "%";
        }
    
    }
}


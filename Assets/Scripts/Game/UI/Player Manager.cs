using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject upgradeOverlay;
    
    public int playerLevel = 1;
    private int currentExp = 0;
    private int requiredExp = 5;    

    public TMP_Text playerLevelText;
    
    public void GainExp()
    {
        currentExp++;
        
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            requiredExp = (int)(requiredExp * 1.5f);
            
            playerLevel++;
            playerLevelText.text = "Уровень " + playerLevel.ToString();

            gameManager.PauseGame();
            upgradeOverlay.SetActive(true);
        }
    }
    
    
}


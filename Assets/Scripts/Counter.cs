using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Counter : MonoBehaviour
{
    public Claw clawScript;
    private BabushkaMain babushkaMain;
    
    private int currentNumOfBabushkas;
    public float chanceToDouble;
    public TMP_Text counter;
    
    public TMP_Text playerLevelText;
    private int expRequired = 5;
    private int experience;
    private int playerLevel;

    private bool isPaused;
    public GameObject upgradeOverlay;
    
    
    void Start()
    {
        upgradeOverlay.SetActive(false);
        
        chanceToDouble = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        babushkaMain = other.GetComponent<BabushkaMain>();
        if (other.CompareTag("Babushka") && babushkaMain.canBeDeleted)
        {
            Destroy(other.GameObject());
            GainExp();
            
            currentNumOfBabushkas += 1;
            
            float randomValue = Random.Range(0f, 100f);

            if (randomValue <= chanceToDouble)
            {
                currentNumOfBabushkas += 1;
            }
            
            counter.text = "Babushkas Collected " + currentNumOfBabushkas.ToString();
        }
    }

    
    private void GainExp()
    {
        experience += 1;
        
        CheckLvlUp();
    }

    private void CheckLvlUp()
    {
        if (experience >= expRequired)
        {
            playerLevel++;
            experience -= expRequired;
            expRequired = (int)(expRequired * 1.5f); 
            
            PauseGame();
            playerLevelText.text = "Level " + playerLevel.ToString();
            Debug.Log("Congratulations! You reached level " + playerLevel);
            
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        upgradeOverlay.SetActive(true);
        
        Debug.Log("Game paused");
    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1f;
        isPaused = false;
        upgradeOverlay.SetActive(false);
        
        Debug.Log("Game resumed");
        
    }


    public void RandomMultiply()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= chanceToDouble)
        {
            currentNumOfBabushkas += 1;
        }
        
    }
    
    
}

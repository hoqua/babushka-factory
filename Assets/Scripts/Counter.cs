using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public Claw clawScript;
    
    private int currentNumOfBabushkas = 0;
    public TMP_Text counter;
    
    public TMP_Text playerLevelText;
    private int expRequired = 5;
    private int experience = 0;
    private int playerLevel = 0;

    private bool isPaused = false;
    public GameObject upgradeOverlay;

    private BabushkaMain babushkaMain;
    void Start()
    {
        upgradeOverlay.SetActive(false);
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        babushkaMain = other.GetComponent<BabushkaMain>();
        if (other.CompareTag("Babushka") && babushkaMain.canBeDeleted)
        {
            Destroy(other.GameObject());
            GainExp();
            
            currentNumOfBabushkas += 1;
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
    
    
    
}

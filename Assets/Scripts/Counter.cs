using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    private int currentNumOfBabushkas = 0;
    public TMP_Text counter;
    
    public TMP_Text playerLevelText;
    private int expRequired = 5;
    private int experience = 0;
    private int playerLevel = 0;
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Babushka"))
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
            playerLevelText.text = "Level " + playerLevel.ToString();
            
            Debug.Log("Congratulations! You reached level " + playerLevel);
        }
    }
}

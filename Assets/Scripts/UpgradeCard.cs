using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public Claw clawScript;
    public Counter upgradeScript;
    public Spawner spawnerScript;
    
    private float clawSpeedInitial;
    private float intervalInitial;

    private void Start()
    {
        clawSpeedInitial = clawScript.clawSpeed;
        intervalInitial = spawnerScript.interval;
    }

    private void OnMouseDown()
    {
        
        switch (gameObject.name)
        {
            case "Card - FastClaw":
                clawScript.clawSpeed += clawSpeedInitial * 0.1f;
                upgradeScript.ResumeGame();
                break;
            
            case "Card - DoubleBabushkas":
                upgradeScript.chanceToDouble += 1f;
                upgradeScript.ResumeGame();
                break;
            
            case "Card - SpawnRate":
                spawnerScript.interval -= intervalInitial * 0.05f;
                upgradeScript.ResumeGame();
                break;
        }
        
       
    }
}

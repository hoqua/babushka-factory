using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public Claw clawScript;
    public Counter upgradeScript;
    
    private void OnMouseDown()
    {
        clawScript.clawSpeed *= 1.1f;

        upgradeScript.ResumeGame();
    }
}

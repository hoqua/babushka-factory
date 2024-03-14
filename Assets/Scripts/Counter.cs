using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    private int currentNum = 0;
    public TMP_Text counter;
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Babushka"))
        {
            Destroy(other.GameObject());
            currentNum += 1;
            counter.text = currentNum.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject babushkaPrefab;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Key pressed! Spawning babushka!");
            Instantiate(babushkaPrefab, transform.position, Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(GameObject.Find("Babushka Purple(Clone)"));
        }
        
    }
}

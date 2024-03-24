using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _timer;
    public float interval = 2f;
    public GameObject babushkaPrefab;
    
    private void Start()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= interval)
        {
            Instantiate(babushkaPrefab, transform.position, Quaternion.identity);
            
            _timer = 0f;
        }
        
    }
}

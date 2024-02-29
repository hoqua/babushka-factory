using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabushkaMovement : MonoBehaviour
{
    public Animator animation;
    
    void Start()
    {
        animation = GetComponent<Animator>();
    } 
    
    //This triggers babushka's animations if she collides with conveyor
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Conveyor")
        {
            animation.SetBool("isPushed", true);
        }
        else
        {
            animation.SetBool("isPushed", false);
        }
    }

}

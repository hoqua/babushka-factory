using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabushkaMovement : MonoBehaviour
{
    private Animator animation;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        animation = GetComponent<Animator>();
    } 

    private bool IsOnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void Update()
    {
        if (IsOnGround())
        {
            animation.SetBool("isPushed", true);
        }
        else
        {
            animation.SetBool("isPushed", false);
        }
    }

}

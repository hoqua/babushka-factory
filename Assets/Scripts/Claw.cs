using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private Vector2 initialPosition;
    public float speed = 5f;
    private Vector2 targetPosition; // Позиция, к которой объект должен двигаться
    private MovingDirection? movingDirection = null;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && movingDirection == null) 
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            movingDirection = MovingDirection.Horizontal;
        }
        
        if (movingDirection == MovingDirection.Horizontal)
        {
            MoveHorizontal();
        }
        if (movingDirection == MovingDirection.Down)
        {
            MoveDown();
        }
        if (movingDirection == MovingDirection.Up)
        {
            MoveUp();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Conveyor"))
        {
            movingDirection = MovingDirection.Up;
        }
    }

    void MoveHorizontal()
    {
        var horizontalTarget = new Vector2(targetPosition.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, horizontalTarget, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.x - horizontalTarget.x) < 0.0001f)
        {
            movingDirection = MovingDirection.Down;
        }
    }

    void MoveDown()
    {
        var verticalTarget = new Vector2(transform.position.x, targetPosition.y);
        transform.position = Vector3.MoveTowards(transform.position, verticalTarget, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
        {
            movingDirection = MovingDirection.Up;
        }
    }
    
    void MoveUp()
    {
        var verticalTarget = new Vector2(transform.position.x, initialPosition.y);
        transform.position = Vector3.MoveTowards(transform.position, verticalTarget, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.y - verticalTarget.y) < 0.0001f)
        {
            movingDirection = null;
        }
    }
}


enum MovingDirection
{
    Horizontal, 
    Up,
    Down
}
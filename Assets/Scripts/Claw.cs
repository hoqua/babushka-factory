using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public float speed = 5f;
    bool isMoving = false; // Флаг, указывающий, двигается ли объект в данный момент
    Vector3 targetPosition; // Позиция, к которой объект должен двигаться

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving) 
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition.y = transform.position.y; // Блокируем изменение по y
            targetPosition.z = transform.position.z; // Блокируем изменение по Z 

            isMoving = true;
            
        }

        if (isMoving)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            
            if (transform.position == targetPosition)
            {
                isMoving = false; 
            }
        }
    }
}

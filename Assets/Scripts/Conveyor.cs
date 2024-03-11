using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public float speed;
    Rigidbody2D rBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = rBody.position;
        rBody.position += Vector2.left * (speed * Time.fixedDeltaTime);
        rBody.MovePosition(pos);
    }
}

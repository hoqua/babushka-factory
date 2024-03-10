using System;
using UnityEngine;

public class BabushkaMovement : MonoBehaviour
{
    public new Animator animation;
    private static readonly int IsPushed = Animator.StringToHash("isPushed");
    public Rigidbody2D babushka;

    void Start()
    {
        animation = GetComponent<Animator>();
    } 
    
    //Триггерит анимацию хождения когда бабушка задевает конвейер
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deleter"))
        {
            Destroy(GameObject.Find("Babushka Purple(Clone)"));
        }
        
        if (other.CompareTag("Conveyor"))
        {
            animation.SetBool(IsPushed, true);
        }
        
        else
        {
            animation.SetBool(IsPushed, false);
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Claw"))
        {
            babushka = GetComponent<Rigidbody2D>();
            babushka.isKinematic = true;
            Debug.Log("Gotcha!");
            animation.SetBool(IsPushed, false);
            transform.parent = GameObject.Find("Claw").transform;
            transform.parent.gameObject.SetActive(true);
        }
        
    }
    
    //Отключает анимацию пока бабушка в клешне
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Claw"))
        {
            animation.SetBool(IsPushed, false);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class BabushkaMain : MonoBehaviour
{
    public new Animator animation;
    private Rigidbody2D babushka;
    
        
    public bool canBeDeleted;
    private static readonly int IsPushed = Animator.StringToHash("isPushed");

    void Start()
    {
        animation = GetComponent<Animator>();
        
    } 
    
    //Триггерит анимацию ходьбы пока бабушка на конвейере
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Conveyor"))
        {
            animation.SetBool(IsPushed, true);
            
        }

       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Conveyor"))
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
            animation.SetBool(IsPushed, false);
            
            canBeDeleted = true;
        }
        else
        {
            canBeDeleted = false;
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

using UnityEngine;

public class BabushkaMovement : MonoBehaviour
{
    public new Animator animation;
    private static readonly int IsPushed = Animator.StringToHash("isPushed");
    private Rigidbody2D babushka;
    Collider2D myCollider;
   
    void Start()
    {
        animation = GetComponent<Animator>();
        
    } 
    
    //This triggers babushka's animations if she collides with conveyor
    void OnTriggerEnter2D(Collider2D other)
    {
        var isAnimationEnabled = other.CompareTag("Conveyor");
        animation.SetBool(IsPushed, isAnimationEnabled);
        
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Claw"))
        {
            babushka = GetComponent<Rigidbody2D>();
            babushka.isKinematic = true;
            animation.SetBool(IsPushed, false);
            transform.parent = GameObject.Find("Claw").transform;
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

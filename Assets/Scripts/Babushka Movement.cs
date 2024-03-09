using UnityEngine;

public class BabushkaMovement : MonoBehaviour
{
    public new Animator animation;
    private static readonly int IsPushed = Animator.StringToHash("isPushed");

    void Start()
    {
        animation = GetComponent<Animator>();
    } 
    
    //This triggers babushka's animations if she collides with conveyor
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deleter"))
        {
            Destroy(GameObject.Find("Babushka Purple(Clone)"));
        }
        
        else if (other.CompareTag("Conveyor"))
        {
            animation.SetBool(IsPushed, true);
        }
        
        else
        {
            animation.SetBool(IsPushed, false);
        }

        
    }

}

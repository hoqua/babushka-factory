using UnityEngine;

namespace Features.Conveyor.Scripts
{
    public class Conveyor : MonoBehaviour
    {
        public new Animator animation;
        private static readonly int IsWorking = Animator.StringToHash("isWorking");  
        
        public float speed;
        Rigidbody2D rBody;
        
        // Start is called before the first frame update
        void Start()
        {
            animation.SetBool(IsWorking, true);
            rBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 pos = rBody.position;
            rBody.position += Vector2.left * (speed * Time.fixedDeltaTime);
            rBody.MovePosition(pos);
        }

        public void DisableConveyor()
        {
            animation.SetBool(IsWorking, false);
            enabled = false;
        }

        public void EnableConveyor()
        {
            animation.SetBool(IsWorking, true);
            enabled = true;
        }
    }
}

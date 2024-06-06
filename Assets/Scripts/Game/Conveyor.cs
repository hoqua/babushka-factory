using UnityEngine;

namespace Game
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
            enabled = false;
            animation.SetBool(IsWorking, false);
        }

        public void EnableConveyor()
        {
            enabled = true;
            animation.SetBool(IsWorking, true);
        }
    }
}

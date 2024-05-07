using UnityEngine;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        private float _timer;
        public float interval;
    
        public GameObject babushkaPurplePrefab;

        private void Start()
        {
            _timer = 0f;
            interval = 2f;
        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;
        
            if (_timer >= interval)
            {
                GameObject newBabushka = Instantiate(babushkaPurplePrefab, transform.position, Quaternion.identity);
                newBabushka.name = "Babushka Purple";
            
                _timer = 0f;
            }
        
        }
    }
}

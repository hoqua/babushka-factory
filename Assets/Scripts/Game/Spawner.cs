using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        private float _timer;
        public float interval;

        private const float MinX = -9f;
        private const float MaxX = -1f;
            
        public GameObject babushkaPurplePrefab;
        public List<BabushkaMain> babushkas = new List<BabushkaMain>();
        
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
                float randomSpawnPosition = Random.Range(MinX, MaxX);
                transform.position = new Vector2(randomSpawnPosition, transform.position.y);
                
                GameObject newBabushka = Instantiate(babushkaPurplePrefab, transform.position, Quaternion.identity);
                newBabushka.name = "Babushka Purple";
                
                BabushkaMain babushkaMainScript = newBabushka.GetComponent<BabushkaMain>();
                if (babushkaMainScript != null)
                {
                    babushkaMainScript.walkingSpeed = Random.Range(0.5f, 2.5f);
                    babushkas.Add(babushkaMainScript);
                }
                
                _timer = 0f;
            }
        
        }
        
    }
    
}

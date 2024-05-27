using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        private float _timer;
        public float interval;

        private const float MinX = -7f;
        private const float MaxX = 7f;
        private float minSpeed = 0.5f;
        private float maxSpeed = 3f;
            
        public GameObject babushkaPurplePrefab;
        public GameObject repairTool;
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

                GameObject prefabToSpawn = GetRandomPrefab();
                GameObject newPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
                newPrefab.name = prefabToSpawn.name;
                
                BabushkaMain babushkaMainScript = newPrefab.GetComponent<BabushkaMain>();
                if (babushkaMainScript != null && prefabToSpawn == babushkaPurplePrefab)
                {
                    float normalized = (transform.position.x - MinX) / (MaxX - MinX);
                    babushkaMainScript.walkingSpeed = Mathf.Lerp(minSpeed, maxSpeed, normalized);
                    babushkas.Add(babushkaMainScript);
                }
                
                _timer = 0f;
            }
            
        }

        private GameObject GetRandomPrefab()
        {
            var randomValue = Random.value;

            if (randomValue < 0.1f)
            {
                return repairTool;
            }
            else
            {
                return babushkaPurplePrefab;
            }
        }

        public void CloneBabushkas()
        {
            var newBabushkas = new List<BabushkaMain>();
            if (newBabushkas == null) throw new ArgumentNullException(nameof(newBabushkas));

            foreach (BabushkaMain babushka in babushkas)
            {
                Vector2 spawnPosition = babushka.transform.position;
                GameObject clonedBabushka = Instantiate(babushkaPurplePrefab, spawnPosition, Quaternion.identity);
            
                BabushkaMain clonedBabushkaMainScript = clonedBabushka.GetComponent<BabushkaMain>();
                if (clonedBabushkaMainScript != null)
                {
                    clonedBabushkaMainScript.walkingSpeed = babushka.walkingSpeed;
                    newBabushkas.Add(clonedBabushkaMainScript);
                }
            }
        }

        public void RemoveBabushka(BabushkaMain babushka)
        {
            if (babushkas.Contains(babushka))
            {
                babushkas.Remove(babushka);
            }
        }
        
    }
    
}

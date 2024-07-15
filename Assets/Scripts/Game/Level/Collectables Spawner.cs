using System.Collections.Generic;
using Features.Babushka_Basic.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Level
{
    public class CollectablesSpawner : MonoBehaviour
    {
        public Deleter deleterScript;
        
        private float _timer;
        public float interval;

        private const float MinX = -7f;
        private const float MaxX = 7f;
        private const float MinSpeed = 0.5f;
        private const float MaxSpeed = 3f;

        public GameObject babushkaPrefab;
        public GameObject repairTool;
        public GameObject cookieBox;
        public List<BabushkaMain> babushkas = new();
        
        public delegate void BabushkaSpawnedDelegate(BabushkaMain babushka);
        public event BabushkaSpawnedDelegate OnBabushkaSpawned;

        public int spawnedBabushkas = 0;
    
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
                if (babushkaMainScript != null && prefabToSpawn == babushkaPrefab)
                {
                    float normalized = (transform.position.x - MinX) / (MaxX - MinX);
                    babushkaMainScript.walkingSpeed = Mathf.Lerp(MinSpeed, MaxSpeed, normalized);
                    babushkas.Add(babushkaMainScript);
                    
                    OnBabushkaSpawned?.Invoke(babushkaMainScript);
                }
                
                _timer = 0f;
            }
            
        }

        private GameObject GetRandomPrefab()
        {
            var randomValue = Random.value;
            if (randomValue < 0.05f)
            {
                return cookieBox;
            }

            if (randomValue < 0.1f)
            {
                return repairTool;
            }
            
            spawnedBabushkas++;
                
            deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / spawnedBabushkas) * 100f);
            deleterScript.deletedCounterText.text = "Упущено бабушек " + deleterScript.deletedBabushkasRatio + "%";
                
            return babushkaPrefab;
        }

        public void CloneBabushkas()
        {
            foreach (BabushkaMain babushka in babushkas)
            {
                if (babushka == null) continue;
                
                Vector2 spawnPosition = babushka.transform.position;
                GameObject clonedBabushka = Instantiate(babushkaPrefab, spawnPosition, Quaternion.identity);

                BabushkaMain clonedBabushkaMainScript = clonedBabushka.GetComponent<BabushkaMain>();
                if (clonedBabushkaMainScript != null)
                {
                    clonedBabushkaMainScript.walkingSpeed = babushka.walkingSpeed;
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

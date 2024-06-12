using UnityEngine;

namespace Resources.Effects.Eater.Script
{
  public class EaterSpawner : MonoBehaviour
  {
    public GameObject objectPrefab;
    public float spawnXPosition = 10f;
    public float moveSpeed = 5f;
    public float destroyTime = 10f;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        SpawnEater();
      }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void SpawnEater()
    {
      Vector2 spawnPosition = new Vector2(spawnXPosition, transform.position.y);
      GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

      EaterObject eaterObjectScript = spawnedObject.AddComponent<EaterObject>();
      eaterObjectScript.moveSpeed = moveSpeed;
      eaterObjectScript.destroyTime = destroyTime;
    }
    
  }
  
}

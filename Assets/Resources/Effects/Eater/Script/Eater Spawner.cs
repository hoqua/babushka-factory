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

    void SpawnEater()
    {
      Vector2 spawnPosition = new Vector2(spawnXPosition, transform.position.y);
      GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
      
      StartCoroutine(MoveEaterLeft(spawnedObject));
    }

    System.Collections.IEnumerator MoveEaterLeft(GameObject obj)
    {
      float elapsedTime = 0f;

      while (elapsedTime < destroyTime)
      {
        obj.transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        elapsedTime += Time.deltaTime;
        yield return null;
      }
      
      Destroy(obj);
    }
    
  }
  
}

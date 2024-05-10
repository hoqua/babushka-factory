using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public Transform[] cardPositions;
    
    
  
    public void ShowUpgradeCards()
    {
        
        List<int> cardIndices = new List<int>();

        for (int i = 0;  i < cardPrefabs.Length;  i++)
        {
            cardIndices.Add(i);
        }

        ShuffleList(cardIndices);

        List<int> selectedIndices = cardIndices.GetRange(0, Mathf.Min(3, cardIndices.Count));

        for (int i = 0; i < selectedIndices.Count; i++)
        {
            GameObject cardInstance = Instantiate(cardPrefabs[selectedIndices[i]]);
            
            cardInstance.transform.position = cardPositions[i].position;
        }
    }
    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            var temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

}
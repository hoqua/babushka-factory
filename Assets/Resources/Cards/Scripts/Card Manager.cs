using System.Collections.Generic;
using Game.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Cards.Scripts
{
    public class CardManager : MonoBehaviour
    {
        public PlayerManager playerManager;
    
        public List<GameObject> cardPrefabs = new List<GameObject>();
        public Transform[] cardPositions;

        private void Start()
        {
            GameObject[] prefabs = UnityEngine.Resources.LoadAll<GameObject>("Cards/1 level"); //Добавляет в пулл карточки 1 уровня
            cardPrefabs.AddRange(prefabs);
        }

        public void ShowUpgradeCards()
        {
            AddNewCards();  
            
            List<int> cardIndices = new List<int>();

            for (int i = 0;  i < cardPrefabs.Count;  i++)
            {
                cardIndices.Add(i);
            }

            ShuffleList(cardIndices);

            List<int> selectedIndices = cardIndices.GetRange(0, Mathf.Min(3, cardIndices.Count));

            for (int i = 0; i < selectedIndices.Count; i++)
            {
                GameObject cardInstance = Instantiate(cardPrefabs[selectedIndices[i]], cardPositions[i].position, Quaternion.identity);
                cardInstance.name = cardInstance.name.Replace("(Clone)", ""); //Убирает (Clone) из имени карточки
            }
        }
        
        //Перемешивает лист карточек, чтобы они всегда появлялись на разных позициях
        private void ShuffleList(List<int> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
       
        //Добавляет новые карточки, если игрок достиг нужного уровня
        private void AddNewCards()
        {
            if (playerManager.playerLevel == 5)
            {
                GameObject[] prefabs5 = UnityEngine.Resources.LoadAll<GameObject>("Cards/5 level"); //Добавляет в пулл карточки 5 уровня
                cardPrefabs.AddRange(prefabs5);
            }

            if (playerManager.playerLevel == 10)
            {
                GameObject[] prefabs10 = UnityEngine.Resources.LoadAll<GameObject>("Cards/10 level"); //Добавляет в пулл карточки 10 уровня
                cardPrefabs.AddRange(prefabs10);
            }
         
        }
        
    }
}
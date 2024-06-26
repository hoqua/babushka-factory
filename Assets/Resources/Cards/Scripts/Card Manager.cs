using System.Collections;
using System.Collections.Generic;
using Features.Claw.Scripts;
using Game;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Cards.Scripts
{
    public class CardManager : MonoBehaviour
    {
        public PlayerManager playerManager;
        public Claw clawScript;
        
        public List<GameObject> cardPrefabs = new List<GameObject>();
        public Transform[] cardPositions;
        
        private Dictionary<string, int> _cardClickCounts = new Dictionary<string, int>();
        
        private void Start()
        {
            GameObject[] prefabs = UnityEngine.Resources.LoadAll<GameObject>("Cards/1 level"); //Добавляет в пулл карточки 1 уровня
            cardPrefabs.AddRange(prefabs);
        }
        
        //Карточки на которые не действуют ограничения и их можно вызывать всегда.
        public List<string> cardsExemptFromLimit = new List<string>
        {
            "Card - CollectAll",
            "Card - FreezeConveyor",
            "Card - CloneEveryone",
            "Card - SlowDownBabushka",
            "Card - Test"
        };

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

        public void BlockClawInput()
        {
            BoxCollider2D[] colliders2D = clawScript.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D collider in colliders2D)
            {
                collider.enabled = false;
            }
            
            CircleCollider2D[] circleColliders2D = clawScript.GetComponentsInChildren<CircleCollider2D>();
            foreach (CircleCollider2D collider in circleColliders2D)
            {
                collider.enabled = false;
            }
            
            clawScript.isInputBlocked = true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator UnblockClawInput(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            BoxCollider2D[] colliders2D = clawScript.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D collider in colliders2D)
            {
                collider.enabled = true;
            }
            
            CircleCollider2D[] circleColliders2D = clawScript.GetComponentsInChildren<CircleCollider2D>();
            foreach (CircleCollider2D collider in circleColliders2D)
            {
                collider.enabled = true;
            }
            
            clawScript.isInputBlocked = false;
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

        public void IncrementCardClickCount(string cardName)
        {
            if (_cardClickCounts.ContainsKey(cardName))
            {
                _cardClickCounts[cardName]++;
            }
            else
            {
                _cardClickCounts[cardName] = 1;
            }

            Debug.Log($"Card '{cardName}' clicked {_cardClickCounts[cardName]} times.");
            
            if (_cardClickCounts[cardName] >= 2 && !cardsExemptFromLimit.Contains(cardName))
            {
                RemoveCardFromPrefabs(cardName);
                Debug.Log($"Card '{cardName}' removed from prefabs.");
            }
            
        }

        private void RemoveCardFromPrefabs(string cardName)
        {
            Debug.Log($"Removing card '{cardName}' from prefabs.");
            cardPrefabs.RemoveAll(card => card.name == cardName);
            
            
            
            GameObject[] cardsToRemove = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject card in cardsToRemove)
            {
                if (card.name == cardName)
                {
                    DestroyImmediate(card); 
                }
            }
        }
    }
}
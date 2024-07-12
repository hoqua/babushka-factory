using System.Collections;
using System.Collections.Generic;
using Features.Claw.Scripts;
using Game;
using Game.Level;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Cards.Scripts
{
    public class CardManager : MonoBehaviour
    {
        public PlayerManager playerManager;
        public Claw clawScript;
        public CollectablesSpawner collectablesSpawnerScript;
        
        public List<GameObject> cardPrefabs = new List<GameObject>();
        public Transform[] cardPositions;
        
        private readonly Dictionary<string, int> _cardClickCounts = new Dictionary<string, int>();
        public int maxUpgradesPerCard = 10;

        public float clawSpeedInitial;
        public float intervalInitial;
        private void Start()
        {
            clawSpeedInitial = clawScript.clawSpeed;
            intervalInitial = collectablesSpawnerScript.interval;
            
            GameObject[] prefabs = UnityEngine.Resources.LoadAll<GameObject>("Cards/1 level"); //Добавляет в пулл карточки 1 уровня
            cardPrefabs.AddRange(prefabs);
        }
        
        //Карточки на которые не действуют ограничения и их можно вызывать всегда.
        public List<string> cardsExemptFromLimit = new List<string>
        {
            "Card - CollectAll",
            "Card - FreezeConveyor",
            "Card - CloneEveryone",
            "Card - SlowDownBabushka"
        };

        //Обновление текста карточек в зависимости от того сколько раз на них нажали
        private void UpdateCardText(GameObject cardInstance)
        {
            string cardName = cardInstance.name;
            int clickCount = GetCardClickCount(cardName);

            Transform bodyTransform = cardInstance.transform.Find("Body");
            TextMeshPro cardText = bodyTransform.GetComponentInChildren<TextMeshPro>();
            
            //Projectile
            if (cardName == "Card - Projectile" && clickCount >= 1)
            {
                cardText.text = "Уменьшает интервал появления спутников на 1 секунду";
            }

            //Magnet
            if (cardName == "Card - Magnet" && clickCount >= 1)
            {
                cardText.text = "Немного увеличивает радиус и силу магнита";
            }
            
            //SpringWall
            if (cardName == "Card - SpringWall")
            {
                if (clickCount == 1)
                {
                    cardText.text = "Стен становится две";
                }

                if (clickCount >= 2)
                {
                    cardText.text = "Увеличивает силу отталкивания";
                }
                
            }
            
            
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
                
                UpdateCardText(cardInstance);
                
                UpgradeCard upgradeCard = cardInstance.GetComponent<UpgradeCard>();
                if (upgradeCard != null)
                {
                    int clickCount = GetCardClickCount(cardInstance.name);
                    upgradeCard.UpdateSquaresColor(clickCount);
                }
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
            
            if (_cardClickCounts[cardName] >= maxUpgradesPerCard && !cardsExemptFromLimit.Contains(cardName))
            {
                RemoveCardFromPrefabs(cardName);
                Debug.Log($"Card '{cardName}' removed from prefabs.");
            }
            
        }
        
        public int GetCardClickCount(string cardName)
        {
            if (_cardClickCounts.ContainsKey(cardName))
            {
                return _cardClickCounts[cardName];
            }
            return 0;
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
        
        public void BlockClawInput()
        {
            BoxCollider2D[] colliders2D = clawScript.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D boxCollider2D in colliders2D)
            {
                boxCollider2D.enabled = false;
            }
            
            CircleCollider2D[] circleColliders2D = clawScript.GetComponentsInChildren<CircleCollider2D>();
            foreach (CircleCollider2D circleCollider2D in circleColliders2D)
            {
                circleCollider2D.enabled = false;
            }
            
            clawScript.isInputBlocked = true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator UnblockClawInput(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            BoxCollider2D[] colliders2D = clawScript.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D boxCollider2D in colliders2D)
            {
                boxCollider2D.enabled = true;
            }
            
            CircleCollider2D[] circleColliders2D = clawScript.GetComponentsInChildren<CircleCollider2D>();
            foreach (CircleCollider2D circleCollider2D in circleColliders2D)
            {
                circleCollider2D.enabled = true;
            }
            
            clawScript.isInputBlocked = false;
        }

    }
}
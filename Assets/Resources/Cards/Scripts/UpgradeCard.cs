using Game;
using UnityEngine;

namespace Resources.Cards.Scripts
{
    public class UpgradeCard : MonoBehaviour
    {
        public GameManager gameManager;
        public CardManager cardManager;
        
        public SpriteRenderer[] squares;
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            cardManager = FindObjectOfType<CardManager>();
        }
        
        
        //Базовая функция для всех карточек при нажатии на них
        private void OnMouseDown()
        {
            var cardName = gameObject.name;
            
            cardManager.IncrementCardClickCount(cardName);
                
            gameManager.ResumeGame();
            gameManager.HideUpgradeOverlay();
                
            HideCardsFromUpgradeMenu();
        }
        
        public void UpdateSquaresColor(int cardClicksCount)
        {
            Color newColor = GetColorBasedOnClicks(cardClicksCount);
        
            for (int i = 0; i < squares.Length; i++)
            {
                if (i < cardClicksCount)
                {
                    squares[i].color = newColor;
                }
                else
                {
                    squares[i].color = Color.black; // Исходный цвет квадрата
                }
            }
        }

        private Color GetColorBasedOnClicks(int cardClicksCount)
        {
            // Здесь можно задать логику выбора цвета на основе cardClicksCount
            // Пример: чем больше нажатий, тем краснее квадрат
            float t = Mathf.Clamp01(cardClicksCount / 10f);
            return Color.Lerp(Color.green, Color.red, t); // От зеленого к красному
        }
        

        private void HideCardsFromUpgradeMenu()
        {
            UpgradeCard[] allUpgradeCards = FindObjectsOfType<UpgradeCard>();

            foreach (UpgradeCard card in allUpgradeCards)
            {
                Destroy(card.gameObject);
            }
        }
        
    }
}
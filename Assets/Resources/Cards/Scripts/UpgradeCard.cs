using System;
using System.Collections;
using System.Collections.Generic;
using Features.Babushka_Basic.Scripts;
using Features.Claw.Scripts;
using Game;
using Game.Level;
using Resources.Effects.Projectile.Scripts;
using Resources.Effects.Spring_Wall.Scripts;
using TMPro;
using UnityEngine;

namespace Resources.Cards.Scripts
{
    public class UpgradeCard : MonoBehaviour
    {
        public GameManager gameManager;
        public CardManager cardManager;
        public CollectablesSpawner spawnerScript;
        public ProjectileSpawner projectileSpawnerScript;
        public MagnetController magnetController;
        public SpringWallSpawner springWallSpawner;
        public SpringWallEffect springWallEffectScript;
        
        
        
        public SpriteRenderer[] squares;
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            cardManager = FindObjectOfType<CardManager>();
            projectileSpawnerScript = FindObjectOfType<ProjectileSpawner>();
            magnetController = FindObjectOfType<MagnetController>();
            springWallSpawner = FindObjectOfType<SpringWallSpawner>();
            springWallEffectScript = FindObjectOfType<SpringWallEffect>();
            
            
            UpdateText();
        }

        private readonly Dictionary<BabushkaMain, float> _originalSpeed = new Dictionary<BabushkaMain, float>();
        private readonly Dictionary<string, Action> _cardActions;

        public UpgradeCard()
        {
            _cardActions = new Dictionary<string, Action>
            {
                
                { "Card - SlowDownBabushka", () => { //Замедляет скорость передвижения бабушек на 50% в течение 30 секунд
                    StartCoroutine(SlowDownBabushkaTemporary(30f));
                }},
                
                { "Card - Projectile", () => { //Спавнит "спутник" каждые 10 секунд. При попадании в бабушку замедляет её. Повторное взяие уменьшает интервал на одну секунду
                    
                    if (projectileSpawnerScript.enabled)
                    {
                        projectileSpawnerScript.spawnInterval -= 1f;
                        projectileSpawnerScript.RestartSpawningProjectiles();
                    }
                    
                    if (projectileSpawnerScript.enabled == false)
                    {
                        projectileSpawnerScript.enabled = true;
                    }
                    
                }},
                
                
                { "Card - SpringWall", () => { //Призывает стену(-ы) по краям конвейера, которая отталкивает объекты
                    if (springWallSpawner.isSpawnCoroutineActive && springWallSpawner.spawnBothSides)
                    {
                        springWallEffectScript.springForce += 10f;
                    }
                    
                    if (springWallSpawner.isSpawnCoroutineActive)
                    {
                        springWallSpawner.spawnBothSides = true;
                    }
                    
                    springWallSpawner.ActivateWallSpawn();
                    
                }},
                
            };
        }

        
        private void UpdateText()
        {
            Transform bodyTransform = transform.Find("Body");
            TextMeshPro textMeshPro = bodyTransform.GetComponentInChildren<TextMeshPro>();

            if (gameObject.name == "Card - Projectile" && projectileSpawnerScript.enabled)
            {
                textMeshPro.text = "Уменьшает интервал появления спутников на 1 секунду";
            }
            
            if (gameObject.name == "Card - Magnet" && magnetController.magnetEffect.enabled)
            {
                textMeshPro.text = "Немного увеличивает радиус и силу магнита";
            }
            
            if (gameObject.name == "Card - SpringWall" && springWallSpawner.isSpawnCoroutineActive)
            {
                textMeshPro.fontSize = 1f;
                textMeshPro.text = "Стен становится две";
            }
            
                if (gameObject.name == "Card - SpringWall" && springWallSpawner.spawnBothSides)
                {
                    textMeshPro.fontSize = 1f;
                    textMeshPro.text = "Увеличивает силу отталкивания";
                }
                
        }

        private IEnumerator SlowDownBabushkaTemporary(float duration)
        {
            foreach (BabushkaMain babushka in spawnerScript.babushkas)
            {
                _originalSpeed[babushka] = babushka.walkingSpeed;
                babushka.walkingSpeed *= 0.5f;
            }
            
            yield return new WaitForSeconds(duration);

            foreach (BabushkaMain babushka in spawnerScript.babushkas)
            {
                if (_originalSpeed.TryGetValue(babushka, out var value))
                {
                    babushka.walkingSpeed = value;
                }
                
            }
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
using System;
using System.Collections;
using System.Collections.Generic;
using Features.Babushka_Basic.Scripts;
using Features.Claw.Scripts;
using Features.Conveyor.Scripts;
using Game;
using Game.Level;
using Resources.Effects.Eater.Script;
using Resources.Effects.Projectile.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Resources.Cards.Scripts
{
    public class UpgradeCard : MonoBehaviour
    {
        public GameManager gameManager;
        public CardManager cardManager;
        public Conveyor conveyorScript;
        public Claw clawScript;
        public Spawner spawnerScript;
        public Counter counterScript;
        public Deleter deleterScript;
        public ProjectileSpawner projectileSpawnerScript;
        public MagnetController magnetController;
        public EaterSpawner eaterSpawnerScript;
    
        private float _clawSpeedInitial;
        private float _intervalInitial;
        
        
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            cardManager = FindObjectOfType<CardManager>();
            clawScript = FindObjectOfType<Claw>();
            spawnerScript = FindObjectOfType<Spawner>();
            conveyorScript = FindObjectOfType<Conveyor>();
            counterScript = FindObjectOfType<Counter>();
            deleterScript = FindObjectOfType<Deleter>();
            projectileSpawnerScript = FindObjectOfType<ProjectileSpawner>();
            magnetController = FindObjectOfType<MagnetController>();
            eaterSpawnerScript = FindObjectOfType<EaterSpawner>();
            
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;
            
            UpdateText();
        }

        private readonly Dictionary<BabushkaMain, float> _originalSpeed = new Dictionary<BabushkaMain, float>();
        private readonly Dictionary<string, Action> _cardActions;

        public UpgradeCard()
        {
            _cardActions = new Dictionary<string, Action>
            {
                { "Card - FastClaw", () => { //Ускоряет клешню на 5%
                    clawScript!.clawSpeed += _clawSpeedInitial * 0.05f;
                }},
                
                { "Card - SpawnRate", () => { //Ускоряет спавн бабушек на 5%
                    spawnerScript!.interval -= _intervalInitial * 0.05f;
                }},
               
                { "Card - FreezeConveyor", () => { //Замораживает конвейер на 5 секунд
                    if (!conveyorScript!.IsInvoking(nameof(Conveyor.DisableConveyor)))
                    {
                        conveyorScript.DisableConveyor();
                        conveyorScript.Invoke(nameof(conveyorScript.EnableConveyor), 5f);
                    }
                }},
                
                { "Card - SlowDownBabushka", () => { //Замедляет скорость передвижения бабушек на 50% в течение 30 секунд
                    StartCoroutine(SlowDownBabushkaTemporary(30f));
                }},
                
                { "Card - CollectAll", () => {  //Собирает всех бабушек на экране
                    if (eaterSpawnerScript != null) eaterSpawnerScript.SpawnEater();
                }},
                
                { "Card - WidenClaw", () => { //Увеличивает область хватания клешни и саму клешню
                    clawScript!.clawCollider.size += new Vector2(0.05f, 0);
                    
                    Vector3 additionalScale = new Vector3(0.05f, 0.05f, 0.05f);
                    clawScript.clawObject.localScale += additionalScale;
                }},
                    
                { "Card - GrabCapacity", () => { //Увеличивает число подбираемых бабушек на один
                    clawScript!.maxGrabbedBabushkas += 1;
                }},
                
                { "Card - CloneEveryone", () => { //Клонирует всех бабушек на экране
                    spawnerScript!.CloneBabushkas();
                }},
                
                { "Card - Projectile", () => { //Спавнит "спутник" каждые 10 секунд. При попадании в бабушку замедляет её. Повторное взяие уменьшает интервал на одну секунду
                    
                    if (projectileSpawnerScript.enabled)
                    {
                        projectileSpawnerScript.spawnInterval -= 1f;
                    }
                    
                    if (projectileSpawnerScript.enabled == false)
                    {
                        projectileSpawnerScript.enabled = true;
                    }
                    
                }},
                    
                { "Card - Magnet", () => { //Добавляет клешне магнит с небольшим радиусом
                    
                    if (magnetController.magnetEffect.enabled)
                    {
                        magnetController.UpgradeMagnet();
                    }
                    
                    magnetController.EnableMagnet();
                    
                }},
                
                { "Card - Test", () => { //Ничего не делает, Duh 
                    
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

        
        private void OnMouseDown()
        {
            if (_cardActions.ContainsKey(gameObject.name))
            {
                _cardActions[gameObject.name]?.Invoke();
                cardManager.IncrementCardClickCount(gameObject.name);
                
                gameManager.ResumeGame();
                gameManager.HideUpgradeOverlay();
                HideCardsFromUpgradeMenu();
                
            }
            
        }
        

        private void HideCardsFromUpgradeMenu()
        {
            UpgradeCard[] allUpgradeCards = FindObjectsOfType<UpgradeCard>();

            foreach (UpgradeCard card in allUpgradeCards)
            {
                if (card != this || cardManager.cardsExemptFromLimit.Contains(card.gameObject.name))
                {
                    Destroy(card.gameObject);
                }
                else
                {
                    card.gameObject.SetActive(false);
                }
            }
        }
        
    }
}
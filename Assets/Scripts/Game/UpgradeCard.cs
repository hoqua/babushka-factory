using System;
using System.Collections;
using System.Collections.Generic;
using Game.Effects;
using Game.UI;
using UnityEngine;


namespace Game
{
    public class UpgradeCard : MonoBehaviour
    {
        public GameManager gameManager;
        public Conveyor conveyorScript;
        public Claw clawScript;
        public Spawner spawnerScript;
        public Counter counterScript;
        public Deleter deleterScript;
        public ProjectileSpawner projectileSpawnerScript;
        public BabushkaMain babushkaMainScript;
    
        private float _clawSpeedInitial;
        private float _intervalInitial;
        
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            clawScript = FindObjectOfType<Claw>();
            spawnerScript = FindObjectOfType<Spawner>();
            conveyorScript = FindObjectOfType<Conveyor>();
            counterScript = FindObjectOfType<Counter>();
            babushkaMainScript = FindObjectOfType<BabushkaMain>();
            deleterScript = FindObjectOfType<Deleter>();
            projectileSpawnerScript = FindObjectOfType<ProjectileSpawner>();
            
            
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;

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
                        conveyorScript.enabled = false;
                        conveyorScript.Invoke(nameof(Conveyor.EnableConveyor), 5f);
                    }
                }},
                
                { "Card - SlowDownBabushka", () => { //Замедляет скорость передвижения бабушек на 50% в течение 30 секунд
                    StartCoroutine(SlowDownBabushkaTemporary(30f));
                }},
                
                { "Card - CollectAll",  //Собирает всех бабушек на экране
                    CollectAllBabushkas
                },
                
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
                { "Card - Projectile", () => { //Спавнит "спутник" каждые 10 секунд. При попадании в бабушку замедляет её.
                    if (projectileSpawnerScript.enabled == false)
                    {
                        projectileSpawnerScript.enabled = true;
                    } 

                }},
                
                { "Card - Test", () => { //Ничего не делает, Duh 
                    
                }},
                
            };
        }
        
        
        
        void CollectAllBabushkas()
        {
            GameObject[] babushkas = GameObject.FindGameObjectsWithTag("Babushka");

            foreach (GameObject babushka in babushkas)
            {
                if (babushka.activeSelf)
                {
                    Destroy(babushka);
                    
                    counterScript.currentNumOfBabushkas++;
                    counterScript.counterText.text = "Собрано Бабушек " + counterScript.currentNumOfBabushkas;
                    
                    deleterScript.deletedBabushkasRatio = (int)((deleterScript.deletedBabushkasCount / counterScript.currentNumOfBabushkas) * 100f);
                    deleterScript.deletedCounterText.text = "Бабушек было упущено " + deleterScript.deletedBabushkasRatio + "%"; 
                }
            }
                
            spawnerScript.babushkas.Clear();
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
                gameManager.ResumeGame();
                gameManager.HideUpgradeOverlay();
                RemoveCards();
            }
        }

        private void RemoveCards()
        {
            foreach (GameObject card in GameObject.FindGameObjectsWithTag("Upgrade Card"))
            {
                Destroy(card);
            }
        }
        
    }
}
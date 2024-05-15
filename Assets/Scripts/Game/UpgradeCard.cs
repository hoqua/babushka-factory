using System.Collections;
using System.Collections.Generic;
using Game.UI;
using TMPro;
using Unity.VisualScripting;
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
            
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;

        }
        
        Dictionary<BabushkaMain, float> originalSpeed = new Dictionary<BabushkaMain, float>();
        private readonly Dictionary<string, System.Action> cardActions;

        public UpgradeCard()
        {
            cardActions = new Dictionary<string, System.Action>
            {
                { "Card - FastClaw", () => { //Ускоряет клешню на 5%
                    clawScript.clawSpeed += _clawSpeedInitial * 0.05f;
                }},
                
                { "Card - SpawnRate", () => { //Ускоряет спавн бабушек на 5%
                    spawnerScript.interval -= _intervalInitial * 0.05f;
                }},
               
                { "Card - FreezeConveyor", () => 
                { //Замораживает конвейер на 5 секунд
                    if (!conveyorScript.IsInvoking(nameof(Conveyor.DisableConveyor)))
                    {
                        conveyorScript.enabled = false;
                        conveyorScript.Invoke(nameof(Conveyor.EnableConveyor), 5f);
                    }
                }},
                
                { "Card - SlowDownBabushka", () => { //Замедляет скорость передвижения бабушек на 50% в течение 30 секунд
                    StartCoroutine(SlowDownBabushkaTemporary(30f));
                }},
                
                { "Card - GatherAll", () => { //Собирает всех бабушек на экране
                    GatherAllBabushkas();
                }},
                
                { "Card - WidenClaw", () => { //Увеличивает область хватания клешни и саму клешню
                    clawScript.clawCollider.size += new Vector2(0.05f, 0);
                    
                    Vector3 additionalScale = new Vector3(0.05f, 0.05f, 0.05f);
                    clawScript.clawObject.localScale += additionalScale;
                }},
                
                { "Card - Test", () => {
                    
                }},
                
                { "Card - DoubleBabushkas", () => {
                    
                }},
                
            };
        }
        
        
        
        void GatherAllBabushkas()
        {
            GameObject[] babushkas = GameObject.FindGameObjectsWithTag("Babushka");

            foreach (GameObject babushka in babushkas)
            {
                if (babushka.activeSelf)
                {
                    Destroy(babushka);
                    
                    counterScript.currentNumOfBabushkas++;
                    counterScript.counterText.text = "Собрано Бабушек " + counterScript.currentNumOfBabushkas.ToString();
                    
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
                originalSpeed[babushka] = babushka.walkingSpeed;
                babushka.walkingSpeed *= 0.5f;
            }
            
            yield return new WaitForSeconds(duration);

            foreach (BabushkaMain babushka in spawnerScript.babushkas)
            {
                if (originalSpeed.ContainsKey(babushka))
                {
                    babushka.walkingSpeed = originalSpeed[babushka];
                }
                
            }
        }

        
        private void OnMouseDown()
        {
            if (cardActions.ContainsKey(gameObject.name))
            {
                cardActions[gameObject.name]?.Invoke();
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

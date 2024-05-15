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
        public BabushkaMain babushkaMainScript;
    
        private float _clawSpeedInitial;
        private float _intervalInitial;
        
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            clawScript = FindObjectOfType<Claw>();
            spawnerScript = FindObjectOfType<Spawner>();
            conveyorScript = FindObjectOfType<Conveyor>();
            babushkaMainScript = FindObjectOfType<BabushkaMain>();
            
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
                
                { "Card - SlowDownBabushka", () => { //Замедляет скорость передвижения бабушек
                    StartCoroutine(SlowDownBabushkaTemporary(30f));
                }},
                
                { "Card - Test", () => {
                    
                }},
                
                { "Card - DoubleBabushkas", () => {
                    
                }},
                
            };
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

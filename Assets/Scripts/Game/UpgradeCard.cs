using System.Collections;
using System.Collections.Generic;
using Game.UI;
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
    
        private float _clawSpeedInitial;
        private float _intervalInitial;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            clawScript = FindObjectOfType<Claw>();
            spawnerScript = FindObjectOfType<Spawner>();
            conveyorScript = FindObjectOfType<Conveyor>();
            
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;
        }

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
               
                { "Card - FreezeConveyor", () => { //Замораживает конвейер на 5 секунд
                    if (!conveyorScript.IsInvoking(nameof(Conveyor.DisableConveyor)))
                    {
                        conveyorScript.enabled = false;
                        conveyorScript.Invoke(nameof(Conveyor.EnableConveyor), 5f);
                    }
                }},
                
                { "Card - DoubleBabushkas", () => {
                    
                }},
                
                { "Card - Test", () => {
                    
                }},
                
            };
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

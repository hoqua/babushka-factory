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
                { "Card - FastClaw(Clone)", () => {
                    clawScript.clawSpeed += _clawSpeedInitial * 0.05f;
                }},
                
                { "Card - SpawnRate(Clone)", () => {
                    spawnerScript.interval -= _intervalInitial * 0.05f;
                }},
               
                { "Card - FreezeConveyor(Clone)", () => {
                    
                    StartCoroutine(DisableScriptForTime(5f));

                    IEnumerator DisableScriptForTime(float time)
                    {
                        conveyorScript.enabled = false;
                        yield return new WaitForSeconds(time);

                        conveyorScript.enabled = true;
                    }
                    
                }},
                
                { "Card - DoubleBabushkas(Clone)", () => {
                    
                }},
                
                { "Card - Test(Clone)", () => {
                    
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

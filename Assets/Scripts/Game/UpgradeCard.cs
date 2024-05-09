using System.Collections;
using System.Collections.Generic;
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
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;
        }

        private readonly Dictionary<string, System.Action> cardActions;

        public UpgradeCard()
        {
            cardActions = new Dictionary<string, System.Action>
            {
                { "Card - FastClaw", () => {
                    clawScript.clawSpeed += _clawSpeedInitial * 0.05f;
                }},
                
                { "Card - SpawnRate", () => {
                    spawnerScript.interval -= _intervalInitial * 0.05f;
                }},
               
                { "Card - FreezeConveyor", () => {
                    
                    StartCoroutine(DisableScriptForTime(5f));

                    IEnumerator DisableScriptForTime(float time)
                    {
                        conveyorScript.enabled = false;
                        yield return new WaitForSeconds(time);

                        conveyorScript.enabled = true;
                    }
                    
                }},
                
                { "Card - DoubleBabushkas", () => {
                    
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
            }
        }
    }
}

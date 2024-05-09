using UnityEngine;


namespace Game
{
    public class UpgradeCard : MonoBehaviour
    {
        public GameManager gameManager;
        
        public Claw clawScript;
        public Spawner spawnerScript;
    
        private float _clawSpeedInitial;
        private float _intervalInitial;

        private void Start()
        {
            _clawSpeedInitial = clawScript.clawSpeed;
            _intervalInitial = spawnerScript.interval;
        }

        private void OnMouseDown()
        {
        
            switch (gameObject.name)
            {
                case "Card - FastClaw":
                    clawScript.clawSpeed += _clawSpeedInitial * 0.1f;
                    gameManager.ResumeGame();
                    gameManager.HideUpgradeOverlay();
                    break;
                //Пока ничего не делает
                case "Card - DoubleBabushkas":
                    gameManager.ResumeGame();
                    gameManager.HideUpgradeOverlay();
                    break;
            
                case "Card - SpawnRate":
                    spawnerScript.interval -= _intervalInitial * 0.05f;
                    gameManager.ResumeGame();
                    gameManager.HideUpgradeOverlay();
                    break;
            }
        
       
        }
    }
}

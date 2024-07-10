using Features.Claw.Scripts;
using Resources.Cards.Scripts;
using UnityEngine;

namespace Resources.Cards._1_level.FastClaw
{
    public class CardFastClaw : MonoBehaviour
    {
        public Claw clawScript;
        public CardManager cardManager;
        
        void Start()
        {
            clawScript = FindObjectOfType<Claw>();
            cardManager = FindObjectOfType<CardManager>();
        }

        //Ускоряет клешню на 5%
        void OnMouseDown()
        {
            clawScript.clawSpeed += cardManager.clawSpeedInitial * 0.05f;
        }
    }
}

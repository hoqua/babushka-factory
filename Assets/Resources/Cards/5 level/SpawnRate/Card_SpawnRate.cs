using System;
using Game.Level;
using Resources.Cards.Scripts;
using UnityEngine;

namespace Resources.Cards._5_level.SpawnRate
{
    public class CardSpawnRate : MonoBehaviour
    {
        public CollectablesSpawner collectablesSpawnerScript;
        public CardManager cardManager;
        
        private void Awake()
        {
            collectablesSpawnerScript = FindObjectOfType<CollectablesSpawner>();
            cardManager = FindObjectOfType<CardManager>();
        }

        private void OnMouseDown()
        {
            if (collectablesSpawnerScript.interval < 0.25f)
            {
                return;
            }
            collectablesSpawnerScript.interval -= cardManager.intervalInitial * 0.05f;
        }
    }
}

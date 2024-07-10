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
        
        void Start()
        {
            collectablesSpawnerScript = FindObjectOfType<CollectablesSpawner>();
            cardManager = FindObjectOfType<CardManager>();
        }

        private void OnMouseDown()
        {
            collectablesSpawnerScript.interval -= cardManager.intervalInitial * 0.05f;
        }
    }
}

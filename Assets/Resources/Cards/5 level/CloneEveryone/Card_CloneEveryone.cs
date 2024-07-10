using Game.Level;
using UnityEngine;

namespace Resources.Cards._5_level.CloneEveryone
{
    public class CardCloneEveryone : MonoBehaviour
    {

        public CollectablesSpawner collectablesSpawnerScript;
    
        private void Awake()
        {
            collectablesSpawnerScript = FindObjectOfType<CollectablesSpawner>();
        }

        private void OnMouseDown()
        {
            collectablesSpawnerScript.CloneBabushkas();
        }
    }
}

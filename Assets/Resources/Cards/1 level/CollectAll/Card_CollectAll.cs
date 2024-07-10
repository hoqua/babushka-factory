using Resources.Effects.Eater.Script;
using UnityEngine;

namespace Resources.Cards._1_level.CollectAll
{
    public class CardCollectAll : MonoBehaviour
    {
        public EaterSpawner eaterSpawnerScript;

        void Start()
        {
            eaterSpawnerScript = FindObjectOfType<EaterSpawner>();
        }
    
        //Собирает(пожирает) всех бабушек на конвейере
        void OnMouseDown()
        {
            if (eaterSpawnerScript != null) eaterSpawnerScript.SpawnEater();
        }
    }
}

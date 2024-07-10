using System;
using UnityEngine;

namespace Resources.Cards._1_level.SlowDownBabushka
{
    public class CardSlowDownBabushka : MonoBehaviour
    {
        public SlowEffectController slowEffectController;
        
        private void Awake()
        {
            slowEffectController = FindObjectOfType<SlowEffectController>();
        }

        private void OnMouseDown()
        {
            if (slowEffectController != null)
            {
                slowEffectController.StartSlowDownBabushkaCoroutine();
            }
            else
            {
                Debug.LogWarning("SlowEffectController не найден.");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Features.Babushka_Basic.Scripts;
using Game.Level;
using UnityEngine;

namespace Resources.Cards._1_level.SlowDownBabushka
{
    public class SlowEffectController: MonoBehaviour
    {
        public CollectablesSpawner collectablesSpawnerScript;

        public float slowDownDuration = 30f;
        
        private readonly Dictionary<BabushkaMain, float> _originalSpeed = new Dictionary<BabushkaMain, float>();
        private bool _isSlowingDown = false;

        private void Awake()
        {
            collectablesSpawnerScript = FindObjectOfType<CollectablesSpawner>();
        }

        private void OnEnable()
        {
            collectablesSpawnerScript.OnBabushkaSpawned += HandleBabushkaSpawned;
        }

        private void OnDisable()
        {
            collectablesSpawnerScript.OnBabushkaSpawned -= HandleBabushkaSpawned;
        }

        private void HandleBabushkaSpawned(BabushkaMain babushka)
        {
            if (_isSlowingDown && !_originalSpeed.ContainsKey(babushka))
            {
                _originalSpeed[babushka] = babushka.walkingSpeed;
                babushka.walkingSpeed *= 0.5f;
            }
        }

        public void StartSlowDownBabushkaCoroutine()
        { 
            StartCoroutine(SlowDownBabushkaTemporary(slowDownDuration));
        }

        private void Update()
        {
            if (_isSlowingDown)
            {
                // Update the speed of all current babushkas
                foreach (BabushkaMain babushka in collectablesSpawnerScript.babushkas)
                {
                    if (babushka != null && !_originalSpeed.ContainsKey(babushka))
                    {
                        _originalSpeed[babushka] = babushka.walkingSpeed;
                        babushka.walkingSpeed *= 0.5f;
                    }
                }
            }
        }

        private IEnumerator SlowDownBabushkaTemporary(float duration)
        {
            _isSlowingDown = true;

            foreach (BabushkaMain babushka in collectablesSpawnerScript.babushkas)
            {
                if (babushka != null && !_originalSpeed.ContainsKey(babushka))
                {
                    _originalSpeed[babushka] = babushka.walkingSpeed;
                    babushka.walkingSpeed *= 0.5f;
                }
            }

            yield return new WaitForSeconds(duration);

            foreach (BabushkaMain babushka in collectablesSpawnerScript.babushkas)
            {
                if (babushka != null && _originalSpeed.TryGetValue(babushka, out var value))
                {
                    babushka.walkingSpeed = value;
                }
            }

            _originalSpeed.Clear();
            _isSlowingDown = false;
        }
    }
}

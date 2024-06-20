using System.Collections;
using Features.Babushka_Basic.Scripts;
using UnityEngine;

namespace Resources.Effects.Projectile.Scripts
{
    public class FreezeManager : MonoBehaviour
    {
        public static FreezeManager Instance;
        public ProjectileSoundController projectileSoundController;
        private static readonly int IsPushed = Animator.StringToHash("isPushed");

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void FreezeBabushka(BabushkaMain babushkaMainScript, float duration)
        {
            StartCoroutine(FreezeCoroutine(babushkaMainScript, duration));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator FreezeCoroutine(BabushkaMain babushkaMainScript, float duration)
        {
            var originalWalkingSpeed = babushkaMainScript.walkingSpeed;
            RigidbodyType2D originalBodyType = babushkaMainScript._rigidbody.bodyType;

            if (!babushkaMainScript.isFrozen)
            {
                projectileSoundController = FindObjectOfType<ProjectileSoundController>();
                if (projectileSoundController != null)
                {
                    projectileSoundController.PlayFreezeSound();
                }
            }
            
            //Изменение состояния
            babushkaMainScript.isFrozen = true;
            babushkaMainScript.walkingSpeed = 0;
            babushkaMainScript._rigidbody.bodyType = RigidbodyType2D.Static;
            babushkaMainScript.iceBlockSprite.enabled = true;   //Маска ледяной глыбы
            babushkaMainScript.animation.SetBool(IsPushed, false);

            yield return new WaitForSeconds(duration);

            //Возвращение исходного состояния
            babushkaMainScript.walkingSpeed = originalWalkingSpeed;
            babushkaMainScript._rigidbody.bodyType = originalBodyType;
            babushkaMainScript.iceBlockSprite.enabled = false;  //Маска ледяной глыбы
            babushkaMainScript.animation.SetBool(IsPushed, true);
            babushkaMainScript.isFrozen = false;
        }
        
    }
}
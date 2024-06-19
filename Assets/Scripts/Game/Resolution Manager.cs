using UnityEngine;

namespace Game
{
    public class ResolutionManager : MonoBehaviour
    {
        void Start()
        {
            // Получить разрешение экрана устройства
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            bool isFullScreen = Screen.fullScreen;

            // Установить разрешение экрана
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        }
    }
}

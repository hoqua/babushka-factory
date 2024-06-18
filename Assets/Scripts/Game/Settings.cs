using UnityEngine.Audio;
using UnityEngine;

namespace Game
{
    public class Settings : MonoBehaviour
    {
        public AudioMixer mixer;

        public GameObject settingsPanel;
        private bool _isSettingsActive;

        private void Start()
        {
            _isSettingsActive = false;
            settingsPanel.SetActive(false);
        }

        public void ShowSettingMenu()
        {
            if (_isSettingsActive)
            {
                _isSettingsActive = false;
                settingsPanel.SetActive(false);
            }

            else if (!_isSettingsActive)
            {
                _isSettingsActive = true;
                settingsPanel.SetActive(true);
            }
            
        }

        public void ToggleFullscreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        public void SetVolume(float volume)
        {
            mixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        }
        
    }
}

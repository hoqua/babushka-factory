
using System;
using UnityEngine;

namespace Game
{
    public class Settings : MonoBehaviour
    {
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
        
    }
}

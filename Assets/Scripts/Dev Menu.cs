using Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DevMenuController : MonoBehaviour
{
    public GameObject devMenuPanel;
    private bool _isDevMenuVisible = false;

    public PlayerManager playerManager;
    
    private readonly KeyCode[] _konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };
    private int _konamiIndex = 0;
    
    public void CardUpgrade(int val)
    {
        if (val == 0)
        {
            Debug.Log("Опция 1");
        }
        
        if (val == 1)
        {
            Debug.Log("Опция 2");
        }
        
        if (val == 2)
        {
            Debug.Log("Опция 3");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleDevMenu();
        }
        
        CheckKonamiCode();
    }
    
    public void LevelUp()
    {
        playerManager.currentExp = playerManager.requiredExp;
        playerManager.CheckLevelUp();
    }
    
    private void ToggleDevMenu()
    {
        _isDevMenuVisible = !_isDevMenuVisible;
        devMenuPanel.SetActive(_isDevMenuVisible);
    }

    private void CheckKonamiCode()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(_konamiCode[_konamiIndex]))
            {
                _konamiIndex++;

                if (_konamiIndex == _konamiCode.Length)
                {
                    _konamiIndex = 0;
                    ToggleDevMenu();
                }
            }
            else
            {
                _konamiIndex = 0;
            }
        }
    }
    
}
using Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DevMenuController : MonoBehaviour
{
    public GameObject devMenuPanel;
    private bool _isDevMenuVisible = false;

    public PlayerManager playerManager;
    
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
    
}
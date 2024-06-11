using Game;
using UnityEngine;

public class DevMenuController : MonoBehaviour
{
    public GameObject devMenuPanel; 
    private bool _isDevMenuVisible = false;

    public PlayerManager playerManager;

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
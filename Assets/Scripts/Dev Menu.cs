using UnityEngine;
using UnityEngine.UI;

public class DevMenuController : MonoBehaviour
{
    public GameObject devMenuPanel; 
    private bool _isDevMenuVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleDevMenu();
        }
    }

    private void ToggleDevMenu()
    {
        _isDevMenuVisible = !_isDevMenuVisible;
        devMenuPanel.SetActive(_isDevMenuVisible);
    }
    
}
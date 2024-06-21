using Game;
using Resources.Effects.Eater.Script;
using Resources.Effects.Projectile.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DevMenuController : MonoBehaviour
{
    public GameObject devMenuPanel;
    private bool _isDevMenuVisible = false;

    public ProjectileSpawner projectileSpawnerScript;
    public PlayerManager playerManager;
    public EaterSpawner eaterSpawnerScript;
    
    private readonly KeyCode[] _konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };
    private int _konamiIndex = 0;

    void Start()
    {
        devMenuPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleDevMenu();
        }
        
        CheckKonamiCode();
    }

    //Функции меню
    public void SpawnEater() 
    {
        eaterSpawnerScript.SpawnEater();
    }
    
    public void LevelUp()
    {
        playerManager.currentExp = playerManager.requiredExp;
        playerManager.CheckLevelUp();
    }

    public void SpawnSputnik()
    {
        projectileSpawnerScript.SpawnProjectile();
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
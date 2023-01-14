using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Tooltip("Coose which GameSettings asset to use")]
    public GameSettingsScriptableObject _gameSettingsScriptableObject;

    private static GameSettingsScriptableObject _gameSettingsInstance = null;
    public static GameSettingsScriptableObject gameSettingsInstance => _gameSettingsInstance;

    private static Settings _settingsInstance;
    public static Settings settingsInstance => _settingsInstance;

    private void Awake()
    {
        if (Settings._settingsInstance == null)
        {
            Settings._settingsInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Settings._settingsInstance != this)
        {
            Debug.LogWarning("A previously awakened Settings Monobehaviour exists. Destroying instance");
            Destroy(this.gameObject, 0f);
        }

        if (Settings._gameSettingsInstance == null)
        {
            Settings._gameSettingsInstance = _gameSettingsScriptableObject;
        }
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettingsScriptableObject", order = 1)]
public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Debug")] public bool debugMode;

    [Header("Player Variables")] 
    public float upwardsForce = 7.5f;
    public float gravityScale = 2.5f;
}

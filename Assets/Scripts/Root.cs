using System;
using System.Collections;
using System.Collections.Generic;
using Game.World;
using UnityEngine;

/// <summary>
/// в теории этот объект должен висеть в стартовой сцене и агреггировать все общеигровые зависимости на себе
/// но пока что он будет висеть рутом в игровой сцене рядом с GameSceneRoot (это тоже самое, но более узкое)
///
/// DI для бедных 
/// </summary>
public class Root : MonoBehaviour
{
    [SerializeField] private GameSceneRoot gameSceneRoot;

    private string autosaveName = "autosave.json";
    
    private readonly GameWorldSaveManager saveManager = new();

    private void Awake()
    {
        Debug.Log($"Save path: {saveManager.SavePath}");
    }

    private void Start()
    {
        if (saveManager.LoadFile(gameSceneRoot.World, autosaveName) != true)
        {
            var testScene = FindObjectOfType<TestScene>();
            if (testScene != null)
            {
                testScene.Init();
            }
        }
    }

    private void OnDestroy()
    {
        if (saveManager.SaveFile(gameSceneRoot.World, autosaveName) != true)
        {
            Debug.LogError($"Can't save to file '{autosaveName}'");
        }
    }
}

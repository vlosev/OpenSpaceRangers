using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common;
using Common.SceneManagement;
using Game.Entities.StarSystem;
using Game.Visual;
using Game.World;
using GameSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

/// <summary>
/// в теории этот объект должен висеть в стартовой сцене и агреггировать все общеигровые зависимости на себе
/// но пока что он будет висеть рутом в игровой сцене рядом с GameSceneRoot (это тоже самое, но более узкое)
///
/// DI для бедных 
/// </summary>
public class Root : MonoBehaviour
{
    [SerializeField] private TimeMachine timeMachine;
    [SerializeField] private GameTimeMachine gameTimeMachine;
    [SerializeField] private string autoSaveName = "autosave.json"; 
    
    private readonly GameWorldSaveManager saveManager = new();
    private GameWorld world;

    private void Start()
    {
        //TODO: пока что для удобства создаем мир здесь, но позже будем создавать мир прям перед загрузкой или созданием новой игры
        var ctx = new GameWorldContext(gameTimeMachine);
        world = new GameWorld(ctx);

        //пытаем загрузить автосейв, если его у нас нет - создаем новую вселенную
        // if (saveManager.LoadFile(world, autoSaveName) != true)
        {
            WorldGenerator.GenerateWorld(world);
        }

        //открываем пока что карту на первой попавшейся звезде
        var firstSystem = world.Entities.FirstOrDefault(i => i is StarSystemEntity);
        if (firstSystem is StarSystemEntity systemEntity)
        {
            LoadStarSystemScene(systemEntity);
        }
    }

    private void OnDestroy()
    {
        if (saveManager.SaveFile(world, autoSaveName) != true)
        {
            Debug.LogError($"Can't save to file '{autoSaveName}'");
        }
    }

    private void LoadStarSystemScene(StarSystemEntity systemEntity)
    {
        var task = new SceneLoaderTask(this, "StarSystemScene", LoadSceneMode.Additive, scene =>
        {
            Debug.Log($"loaded system scene, rootcount={scene.rootCount}");

            var ctx = new StarSystemSceneRootContext(
                systemEntity, 
                gameTimeMachine);

            var sceneRootObjects = new List<ISceneRootObject>();
            foreach (var rootObject in scene.GetRootGameObjects())
                sceneRootObjects.AddRange(rootObject.GetComponentsInChildren<ISceneRootObject>());

            var stringBuilder = new StringBuilder();
            var stopWatches = new Dictionary<ISceneRootObject, Stopwatch>();

            stringBuilder.AppendLine($"calculate load scene time:");

            int rootObjectsCount = sceneRootObjects.Count;
            foreach (var rootObject in sceneRootObjects)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                stopWatches[rootObject] = stopwatch;
                
                rootObject.Init(ctx, () =>
                {
                    if (stopWatches.TryGetValue(rootObject, out var stopwatch))
                    {
                        stopwatch.Stop();
                        stringBuilder.AppendLine($"\tload scene module '{rootObject.GetType().Name}' elapsed {stopwatch.ElapsedMilliseconds} ms");

                        rootObjectsCount--;
                        if (rootObjectsCount <= 0)
                        {
                            Debug.Log(stringBuilder.ToString());
                        }
                    }
                });
            }
        });
        
        task.Start();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collection;
using Game.Entities.StarSector;
using Game.Entities.StarSystem;
using Game.World;
using TMPro;
using UnityEngine;

/*
 * тестовый игровой уровень, где кипит какая-то жизнь
 */
public class TestScene : MonoBehaviour
{
    [SerializeField] private GameSceneRoot gameSceneRoot;
    [SerializeField] private Transform root;

    [SerializeField] private int createSectorsCount = 3; 
    [SerializeField] private int createSystemsCount = 3; 

    private readonly string[] sectorNames = {
        "Диадема",
        "Арц",
        "Хинкаль",
        "Пелмень",
        "Космонавт",
        "Прокрастинатор"
    };

    private readonly string[] systemNames = {
        "Солнце",
        "Альтаир",
        "Ригель",
        "Хуигель",
        "Превед",
        "Медвед",
        "Ололошечка",
        "Пиздошечка"
    };
    
    private readonly string[] rangerNames = {
        "Васян",
        "Колобок",
        "Чупакабра",
        "Хэллчорт",
        "Суньхуйвчай",
        "Кукуруза"
    };

    public void Init()
    {
        var world = gameSceneRoot.World;
        CreateScene(world);
    }

    private void CreateScene(GameWorld world)
    {
        var sectorNamesQueue = new Queue<string>(sectorNames.ShuffleCollection());
        var systemNamesQueue = new Queue<string>(systemNames.ShuffleCollection());
        
        for (var sectorIndex = 0; sectorIndex < createSectorsCount; ++sectorIndex)
        {
            var sectorName = sectorNamesQueue.Dequeue();

            var starDescription = new StarSectorDescription(sectorName);
            var starSector = world.AddEntity<StarSectorEntity>(starDescription);

            var starsCount = Math.Min(createSystemsCount, systemNamesQueue.Count);
            for (var systemIndex = 0; systemIndex < starsCount; ++systemIndex)
            {
                var systemName = systemNamesQueue.Dequeue();

                var systemEntity = world.AddEntity<StarSystemEntity>(new StarSystemDescription(systemName, starSector.ID));
                starSector.AddStarSystem(systemEntity);
            }
        }
        
        world.BuildLinks(null);
    }
}

using System.Collections.Generic;
using System.Linq;
using Game.Entities.Planet;
using Game.Entities.StarSector;
using Game.Entities.StarSystem;
using Game.World;
using Meta;
using Meta.Scheme;

/*
 * тестовый игровой уровень, где кипит какая-то жизнь
 * позднее это должно стать генератором мира в стартовом положении, когда игрока создает новую игру
 */
public static class WorldGenerator
{
    /// <summary>
    /// первичная генерация мира, если мы не нашли что загрузить
    /// ну или при новой игре вызываем генерацию
    /// </summary>
    /// <param name="world">объект мира</param>
    public static void GenerateWorld(GameWorld world)
    {
        //группируем звезды по секторам
        var sectors = new Dictionary<string, List<Star>>();
        var planets = GameConfig.Planets.ToDictionary(i => i.Name, i => i);
        
        foreach (var star in GameConfig.Stars)
        {
            if (sectors.TryGetValue(star.Sector, out var list) != true)
                sectors.Add(star.Sector, list = new List<Star>());
            list.Add(star);
        }

        //теперь идем по секторам и создаем сектора и звезды в них
        foreach (var sector in sectors)
        {
            var starSectorDescription = new StarSectorDescription(sector.Key);
            var starSectorEntity = world.AddEntity<StarSectorEntity>(starSectorDescription);
            
            foreach (var star in sector.Value)
            {
                var starSystemDescription = new StarSystemDescription(star.Name, starSectorEntity.ID);
                var starSystemEntity = world.AddEntity<StarSystemEntity>(starSystemDescription);
                
                starSectorEntity.AddStarSystem(starSystemEntity);
                
                //создаем планеты в этой системе
                foreach (var planet in star.Planets)
                {
                    if (planets.TryGetValue(planet, out var planetConfig))
                    {
                        var planetDescription = new PlanetDescription(planet, planetConfig.ID);
                        world.AddEntity<PlanetEntity>(planetDescription);
                    }
                }
            }
        }

        world.BuildLinks(null);
    }
}
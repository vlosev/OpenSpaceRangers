using System;
using Common;
using Game.Entities;
using Game.Entities.Planet;
using Game.Entities.StarSystem;
using UnityEngine;

namespace Game.Visual.ViewStorage
{
    public class GameEntityViewFactory : NotifiableMonoBehaviour
    {
        [SerializeField] private GameEntityViewStorage storage;
        [SerializeField] private Transform content;

        public GameEntityView CreateView(IVisibleEntity entity)
        {
            return entity switch
            {
                PlanetEntity planetEntity => CreatePlanetView(planetEntity),
                StarSystemEntity starSystemEntity => CreateStarSystemView(starSystemEntity),

                _ => throw new Exception($"Can't create view for '{entity}'"),
            };
        }

        private GameEntityView CreatePlanetView(PlanetEntity entity)
        {
            return storage.TryGetViewSettings(entity, out PlanetEntityViewSettings viewSettings) 
                ? Instantiate(viewSettings.PlanetEntityView, content) 
                : null;
        }
        
        private GameEntityView CreateStarSystemView(StarSystemEntity entity)
        {
            return storage.TryGetViewSettings(entity, out StarEntityViewSettings viewSettings) 
                ? Instantiate(viewSettings.StarEntityView, content) 
                : null;
        }
    }
}
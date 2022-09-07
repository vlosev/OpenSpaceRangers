using System;
using System.Linq;
using Meta;
using UnityEngine;

namespace Game.Entities.Planet
{
    [Serializable]
    public class PlanetDescription : GameEntityDescription
    {
        [SerializeField] private int planetId;

        public int PlanetId => planetId; 
        public Meta.Scheme.Planet Content { get; private set; }
        
        public PlanetDescription(string planetName, int planetId) : base(planetName, GameEntityType.Planet)
        {
            this.planetId = planetId;
            this.Content = GameConfig.Planets.FirstOrDefault(i => i.ID == planetId);
        }
    }
}
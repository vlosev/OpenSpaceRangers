using System;
using Game.World;

namespace Game.Entities.Planet
{
    public class PlanetEntity : GameEntity<PlanetDescription, PlanetEntityState>
    {
        public PlanetEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }
    }
}
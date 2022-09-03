using System;
using Game.World;

namespace Game.Entities.Asteroid
{
    public class AsteroidEntity : GameEntity<AsteroidDescription, AsteroidEntityState>
    {
        public AsteroidEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }
    }
}
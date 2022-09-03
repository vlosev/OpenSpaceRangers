using System;
using Game.World;

namespace Game.Entities.SpaceStation
{
    public class SpaceStationEntity : GameEntity<SpaceStationDescription, SpaceStationEntityState>
    {
        public SpaceStationEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }
    }
}
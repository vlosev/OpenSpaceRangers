using System;
using Game.World;

namespace Game.Entities.Ship
{
    public partial class ShipEntity : GameEntity<ShipDescription, ShipEntityState>
    {
        public ShipEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }
    }
}
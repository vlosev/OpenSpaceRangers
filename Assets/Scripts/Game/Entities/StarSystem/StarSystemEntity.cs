using System;
using System.Collections.Generic;
using Game.Entities.SpaceStation;
using Game.Entities.StarSector;
using Game.World;

namespace Game.Entities.StarSystem
{
    /// <summary>
    /// звездная система, которая знает о своих кораблях и прочих вещах
    /// </summary>
    public class StarSystemEntity : GameEntity<StarSystemDescription, StarSystemEntityState>
    {
        private Guid starSectorId;
        
        public StarSystemEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }

        protected override void OnInit(StarSystemDescription description)
        {
            starSectorId = description.StarSectorId;
            base.OnInit(description);
        }

        protected override StarSystemEntityState GetState()
        {
            return new StarSystemEntityState
            {
                description = Description,
                ID = ID
            };
        }
    }
}
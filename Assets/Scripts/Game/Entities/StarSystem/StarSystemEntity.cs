using System;
using Game.World;

namespace Game.Entities.StarSystem
{
    /// <summary>
    /// звездная система, которая знает о своих кораблях и прочих вещах
    /// </summary>
    public class StarSystemEntity : GameEntity<StarSystemDescription, StarSystemEntityState>, IVisibleEntity
    {
        private Guid starSectorId;
        
        public string View => "defaultStar";
        
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
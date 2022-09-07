using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.StarSystem;
using Game.World;

namespace Game.Entities.StarSector
{
    public class StarSectorEntity : GameEntity<StarSectorDescription, StarSectorEntityState>
    {
        private readonly List<StarSystemEntity> starSystems = new();

        public IReadOnlyList<StarSystemEntity> Stars => starSystems;

        public StarSectorEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }

        #region serialization implementation

        protected override StarSectorEntityState GetState()
        {
            return new StarSectorEntityState()
            {
                description = Description,
                ID = ID,
                StarSystemsIds = starSystems.Select(i => i.ID).ToArray()
            };
        }

        protected override void OnBuildLinks(StarSectorEntityState state)
        {
            if (state != null)
            {
                foreach (var starSystemId in state.StarSystemsIds)
                    if (World.TryGetEntity(starSystemId, out StarSystemEntity system))
                        starSystems.Add(system);
            }
        }

        #endregion

        public void AddStarSystem(StarSystemEntity systemEntity)
        {
            starSystems.Add(systemEntity);
        }
    }
}
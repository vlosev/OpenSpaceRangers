using System;
using Game.Entities;
using Game.Entities.Asteroid;
using Game.Entities.Planet;
using Game.Entities.Ship;
using Game.Entities.SpaceStation;
using Game.Entities.StarSector;
using Game.Entities.StarSystem;

namespace Game.World
{
    public partial class GameWorld
    {
        private GameEntity CreateEntity(GameEntityDescription description, Guid guid)
        {
            if (description == null)
                throw new Exception("Can't create entity with nullable description");
            
            GameEntity entity = description.EntityType switch
            {
                GameEntityType.Ship => new ShipEntity(guid, this),
                GameEntityType.SpaceStation => new SpaceStationEntity(guid, this),
                GameEntityType.Planet => new PlanetEntity(guid, this),
                GameEntityType.Asteroid => new AsteroidEntity(guid, this),
                GameEntityType.StarSystem => new StarSystemEntity(guid, this),
                GameEntityType.StarSector => new StarSectorEntity(guid, this),
                
                //TODO: тут подумать, патроны/снаряды и прочие штуки это энтити системы или нет
                //в теории, некоторые из них могут сохраняться, например выпущенные ракеты
                //а вот обычные оружия просто стреляют в течении дня и не нуждаются в сохранении
                // GameEntityType.Projectile => expr,
                
                _ => throw new ArgumentOutOfRangeException()
            };

            entity.Init(description);
            
            return entity;
        }
        
        private GameEntity CreateEntity(IGameEntityState state)
        {
            if (state == null)
                throw new Exception("Can't create entity from nullable state");
            
            var entity = CreateEntity(state.Description, state.ID);
            entity.State = state;
            
            return entity;
        }
    }
}
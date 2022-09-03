using System;
using System.Collections.Generic;
using Common;
using Game.Entities;
using Game.Entities.StarSector;
using GameSystems;

namespace Game.World
{
    public class GameWorldContext
    {
        public readonly GameTimeMachine GameTimeMachine;

        public GameWorldContext(GameTimeMachine gameTimeMachine)
        {
            GameTimeMachine = gameTimeMachine;
        }
    }

    public partial class GameWorld : DisposableObject, IGameTimeMachineListener
    {
        private readonly Dictionary<Guid, GameEntity> entities = new();

        public IReadOnlyCollection<GameEntity> Entities => entities.Values;

        public event Action<GameEntity> OnAddEntity;
        public event Action<GameEntity> OnRemoveEntity;
        
        /// <summary>
        /// метод инициализации мира, его нужно дернуть независимо от того, загрузили мы мир или нет
        /// </summary>
        /// <param name="context">контекст мира с зависимостями</param>
        public void Init(GameWorldContext context)
        {
            context.GameTimeMachine.AddHandler(0, this).SubscribeToDispose(this);
        }
        
        public void Update(float dt)
        {
            //TODO: тут будем обновлять весь мир!
        }

        #region add/remove/get
        public bool TryGetEntity<TEntity>(Guid guid, out TEntity entity)
        {
            if (entities.TryGetValue(guid, out var baseEntity) && baseEntity is TEntity entityCast)
            {
                entity = entityCast;
                return true;
            }
            
            entity = default;
            return false;
        }
        
        public TEntity AddEntity<TEntity>(GameEntityDescription description) where TEntity : GameEntity
        {
            var entity = CreateEntity(description, Guid.NewGuid());
            if (entity is TEntity entityCast && entities.TryAdd(entity.ID, entity))
            {
                OnAddEntity?.Invoke(entity);
                return entityCast;
            }

            return null;
        }

        public GameEntity AddEntity(IGameEntityState state)
        {
            var entity = CreateEntity(state);
            if (entity != null && entities.TryAdd(entity.ID, entity))
            {
                OnAddEntity?.Invoke(entity);
                return entity;
            }

            return null;
        }

        public bool RemoveEntity(Guid guid)
        {
            var res = entities.Remove(guid, out var entity); 
            if (res)
            {
                OnRemoveEntity?.Invoke(entity);
            }

            return res;
        }

        public bool RemoveEntity(GameEntity entity)
        {
            return RemoveEntity(entity.ID);
        }
        #endregion

        public void BuildLinks(IReadOnlyDictionary<Guid, IGameEntityState> states)
        {
            if (states != null)
            {
                foreach (var (id, entity) in entities)
                {
                    if (states.TryGetValue(id, out var state) && entity is ILinksBuilder linksBuilder)
                        linksBuilder.BuildLinks(state);
                }
            }
            else
            {
                foreach (var entity in entities.Values)
                {
                    if (entity is ILinksBuilder linksBuilder)
                        linksBuilder.BuildLinks(null);
                }
            }
        }
    }
}
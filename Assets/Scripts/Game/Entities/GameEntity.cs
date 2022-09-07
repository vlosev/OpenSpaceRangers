using System;
using Common;
using Game.World;
using UnityEngine;

namespace Game.Entities
{
    //через этот интерфейс зовем метод построения линков между сущностями, если передали валидный стейт - он поднят,
    //иначе это первичный стейт после создания объекта, тогда приходит null
    public interface ILinksBuilder
    {
        void BuildLinks(IGameEntityState entityState);
    }

    //этот интерфейс является признаком того, что для этого ентити где-то есть визуальное представление
    public interface IVisibleEntity
    {
        string View { get; }
        GameEntityType EntityType { get; }
    }
    
    [Serializable]
    public abstract class GameEntity : DisposableObject
    {
        //публичные поля, которые сетятся в объект один раз и на всю жизнь
        public readonly Guid ID;
        public readonly GameWorld World;

        /// <summary>
        /// тип сущности
        /// </summary>
        public abstract GameEntityType EntityType { get; }

        /// <summary>
        /// название сущности в мире
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// состояние сущности для сериализации/десериализации
        /// </summary>
        public abstract IGameEntityState State { get; set; }

        protected GameEntity(Guid guid, GameWorld world)
        {
            ID = guid;
            World = world;
        }
        
        /// <summary>
        /// иниицализация базовых параметров для сущности, то есть ее создание и инициализация
        /// </summary>
        /// <param name="gameEntityDescription"></param>
        public abstract void Init(GameEntityDescription gameEntityDescription);

        public virtual void BeforeDay()
        {
        }
        
        public virtual void Update(float dt)
        {
        }

        public virtual void AfterDay()
        {
        }
    }
    
    /// <summary>
    /// базовая ентити игровая, это что-то, что живет в мире и может воздействовать на других, в общем не декорация
    /// </summary>
    public abstract class GameEntity<TDescription, TState> : GameEntity, ILinksBuilder
        where TDescription : GameEntityDescription
        where TState : GameEntityState<TDescription>
    {
        private TDescription description;

        public TDescription Description => description;
        public override string Name => description.Name;
        public override GameEntityType EntityType => description.EntityType;

        public override IGameEntityState State
        {
            get => GetState();
            set
            {
                if (value == null)
                    throw new Exception($"Can't deserialize nullable state entity '{Name}'");

                if (value is not TState state)
                    throw new Exception($"Can't deserialize state entity '{Name}'. Can't convert from '{value.GetType().Name}' to '{typeof(TState).Name}'");
                    
                SetState(state);
            }
        }

        protected GameEntity(Guid guid, GameWorld world) : base(guid, world)
        {
        }

        public override void Init(GameEntityDescription description)
        {
            if (description is TDescription castDescription)
            {
                this.description = castDescription;
                OnInit(castDescription);
            }
            else
            {
                throw new Exception($"Ca't cast description from type '{description.GetType()}' to '{typeof(TDescription)}'");
            }
        }

        protected virtual void OnInit(TDescription description)
        {
            // Debug.Log($"init game entity '{this}'");
        }

        protected virtual void OnBuildLinks(TState state)
        {
        }

        protected virtual TState GetState()
        {
            // Debug.Log($"get state from entity '{Name}'");
            return default;
        }

        protected virtual void SetState(TState state)
        {
            // Debug.Log($"set state to entity '{Name}'");
        }
        
        void ILinksBuilder.BuildLinks(IGameEntityState entityState)
        {
            if (entityState == null)
            {
                //первичное построение линков, которое используется после создания мира
                OnBuildLinks(null);
            }
            else
            {
                //последующее построение линков, которое используется после загрузки уровня
                if (entityState is not TState state)
                    throw new Exception($"Can't deserialize state entity '{Name}'. Can't convert from '{entityState.GetType().Name}' to '{typeof(TState).Name}'");
                
                OnBuildLinks(state);
            }
        }

        public override string ToString()
        {
            return $"[{GetType().Name}]: {Name}";
        }
    }
}
using System;
using Common;
using Game.Entities;

namespace Game.Visual
{
    /// <summary>
    /// базовая вьюшка любой игровой визуальной сущности, возможно, кроме ракет и прочих летающих штук
    /// </summary>
    public abstract class GameEntityView : NotifiableMonoBehaviour
    {
        public abstract void Init(GameEntity entity);
    }

    /// <summary>
    /// базовая типизированная сущность, чтобы можно было определить какого типа модель здесь используется
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class GameEntityView<TEntity> : GameEntityView
        where TEntity : GameEntity
    {
        protected TEntity Entity { get; private set; }

        public override void Init(GameEntity entity)
        {
            if (entity is TEntity entityCast)
            {
                Entity = entityCast;
                OnInit(entityCast);
            }
            else
            {
                throw new InvalidCastException($"Cast '{entity.GetType()}' to '{typeof(TEntity)}'");
            }
        }

        protected virtual void OnInit(TEntity entity)
        {
        }
    }
}
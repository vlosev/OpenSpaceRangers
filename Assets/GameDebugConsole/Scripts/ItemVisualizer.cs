using System;
using Common;
using Game.Entities;

namespace GameDebugConsole
{
    public abstract class ItemVisualizer : NotifiableMonoBehaviour
    {
        public abstract void Init(GameEntity entity);

        public abstract void Refresh();
    }
    
    public abstract class ItemVisualizer<T> : ItemVisualizer
        where T : GameEntity
    {
        public override void Init(GameEntity entity)
        {
            if (entity is T typedEntity)
            {
                OnInit(typedEntity);
            }
        }

        protected virtual void OnInit(T entity)
        {
        }
    }
}
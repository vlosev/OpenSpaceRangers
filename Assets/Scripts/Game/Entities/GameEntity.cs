using System;
using Common;
using UnityEngine;

namespace Game.Entities
{
    //базовое описание любой Entity
    public abstract class EntityDescription
    {
        public readonly string Name;

        protected EntityDescription(string name)
        {
            Name = name;
        }
    }

    //промежуточный класс GameEntity нужен для того, чтобы мы могли цеплять его к GameObject
    public abstract class GameEntity : NotifiableMonoBehaviour
    {
    }
    
    /// <summary>
    /// базовая ентити игровая, это что-то, что живет в мире и может воздействовать на других, в общем не декорация
    /// </summary>
    public abstract class GameEntity<TDescription> : GameEntity where TDescription : EntityDescription
    {
        private TDescription entityDescription;

        public string Name => entityDescription.Name;

        public void Init(EntityDescription description)
        {
            if (description is TDescription castDescription)
            {
                entityDescription = castDescription;
                OnInit(castDescription);
            }
            else
            {
                throw new Exception($"Ca't cast description from type '{description.GetType()}' to '{typeof(TDescription)}'");
            }
        }

        protected virtual void OnInit(TDescription description)
        {
            Debug.Log($"init game entity '{this}'");
        }

        public override string ToString()
        {
            return $"[{GetType().Name}]: {Name}";
        }
    }
}
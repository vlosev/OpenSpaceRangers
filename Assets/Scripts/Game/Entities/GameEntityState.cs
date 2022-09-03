using System;
using UnityEditor;
using UnityEngine;

namespace Game.Entities
{
    public interface IGameEntityState
    {
        public Guid ID { get; }
        public GameEntityDescription Description { get; }
    }
    //базовое описание состояния любой Entity
    [Serializable]
    public abstract class GameEntityState<TDescription> : IGameEntityState, ISerializationCallbackReceiver  
        where TDescription : GameEntityDescription
    {
        [SerializeField] private string id; 
        [SerializeField] public TDescription description;

        public Guid ID { get; set; } = Guid.Empty;
        public GameEntityDescription Description => description;
        
        public virtual void OnBeforeSerialize()
        {
            id = ID.ToString();
        }

        public virtual void OnAfterDeserialize()
        {
            if (Guid.TryParse(id, out var guid))
            {
                ID = guid;
            }
            else
            {
                throw new Exception($"Can't parse guid from state '{GetType().Name}'");
            }
        }
    }
}
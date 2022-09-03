using System;
using UnityEngine;

namespace Game.Entities
{
    //базовое описание любой Entity
    [Serializable]
    public abstract class GameEntityDescription
    {
        [SerializeField] private GameEntityType entityType;
        [SerializeField] private string name;

        public GameEntityType EntityType => entityType;
        public string Name => name;

        protected GameEntityDescription(string name, GameEntityType entityType)
        {
            this.name = name;
            this.entityType = entityType;
        }
    }
}
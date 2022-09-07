using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using UnityEngine;

namespace Game.Visual.ViewStorage
{
    [Serializable]
    public class GameEntityViewSettings { }
    
    [Serializable]
    public class GameEntityViewCollectionItem<TViewSettings>
        where TViewSettings : GameEntityViewSettings
    {
        [field: SerializeField] public string ViewId { get; private set; }
        [field: SerializeField] public TViewSettings ViewSettings { get; private set; }
    }

    public abstract class GameEntityViewCollection : ScriptableObject
    {
        public abstract GameEntityType EntityType { get; }

        public abstract GameEntityViewSettings GetEntityViewSettings(IVisibleEntity entity);
    }
    
    public abstract class GameEntityViewCollection<TViewCollectionItem, TViewSettings> : GameEntityViewCollection
        where TViewCollectionItem : GameEntityViewCollectionItem<TViewSettings>
        where TViewSettings : GameEntityViewSettings
    {
        [field: SerializeField] public TViewCollectionItem[] Views { get; private set; }

        public override GameEntityViewSettings GetEntityViewSettings(IVisibleEntity entity)
        {
            var item = Views.FirstOrDefault(i => i.ViewId == entity.View);
            if (item == null)
                throw new KeyNotFoundException($"can't get entity view for entity '{entity}'");
            
            return item.ViewSettings;
        }
    }
}
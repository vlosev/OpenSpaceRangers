using System;
using Game.Entities;
using Game.Entities.StarSystem;
using UnityEngine;

namespace Game.Visual.ViewStorage
{
    [Serializable]
    public class StarEntityViewSettings : GameEntityViewSettings
    {
        [field: SerializeField] public StarEntityView StarEntityView { get; private set; }
    } 
    
    [Serializable]
    public class StarEntityViewCollectionItem : GameEntityViewCollectionItem<StarEntityViewSettings>
    {
    }
        
    [CreateAssetMenu(order = 50, menuName = "Game Assets/Create Star View collection", fileName = "star_view_collection.asset")]
    public class StarEntityViewCollection : GameEntityViewCollection<StarEntityViewCollectionItem, StarEntityViewSettings>
    {
        public override GameEntityType EntityType => GameEntityType.StarSystem;
    }
}
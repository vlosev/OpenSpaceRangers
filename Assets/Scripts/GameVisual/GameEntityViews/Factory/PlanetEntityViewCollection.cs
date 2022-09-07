using System;
using Game.Entities;
using UnityEngine;

namespace Game.Visual.ViewStorage
{
    [Serializable]
    public class PlanetEntityViewSettings : GameEntityViewSettings
    {
        [field: SerializeField] public PlanetEntityView PlanetEntityView { get; private set; }
        [field: SerializeField] public Texture noiseTexture;
        [field: SerializeField] public float radius = 10;
    }

    [Serializable]
    public class PlanetEntityViewCollectionItem : GameEntityViewCollectionItem<PlanetEntityViewSettings>
    {
    }

    [CreateAssetMenu(order = 50, menuName = "Game Assets/Create Planet View collection", fileName = "planet_view_collection.asset")]
    public class PlanetEntityViewCollection : GameEntityViewCollection<PlanetEntityViewCollectionItem, PlanetEntityViewSettings>
    {
        public override GameEntityType EntityType => GameEntityType.Planet;
    }
}
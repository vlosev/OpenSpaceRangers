using System;
using System.Linq;
using Game.Entities;
using UnityEngine;

namespace Game.Visual.ViewStorage
{
    [CreateAssetMenu(order = 50, menuName = "Game Assets/Create game entity view collection", fileName = "game_entity_view_collection.asset")]
    public class GameEntityViewStorage : ScriptableObject
    {
        [SerializeField] private GameEntityViewCollection[] viewCollections;

        public bool TryGetViewSettings<TViewSettings>(IVisibleEntity entity, out TViewSettings viewSettings)
            where TViewSettings : GameEntityViewSettings
        {
            viewSettings = default;

            if (entity == null)
                throw new Exception($"request entity view collection for nullable entity");

            var entityViewCollections = viewCollections.Where(i => i.EntityType == entity.EntityType).ToArray();
            if (entityViewCollections.Any())
            {
                foreach (var collection in entityViewCollections)
                {
                    var settings = collection.GetEntityViewSettings(entity);
                    if (settings is TViewSettings viewSettingsCast)
                    {
                        viewSettings = viewSettingsCast;
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
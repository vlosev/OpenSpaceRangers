using System.Linq;
using Common;
using Game.Entities;
using Game.Entities.Planet;
using Meta;
using UnityEngine;

namespace Game.Visual
{
    public class PlanetEntityView : GameEntityView<PlanetEntity>
    {
        protected override void OnInit(PlanetEntity entity)
        {
            entity.Position.SubscribeChanged(OnPositionChanged, true).SubscribeToDispose(this);
            
            var content = GameConfig.Planets.FirstOrDefault(i => i.ID == entity.Description.PlanetId);
            if (content != null)
            {
                Debug.Log($"init view for planet '{content.Name}' / view: {content.View}");
            }
            else
            {
                Debug.LogError($"planet content not found");
            }
        }

        private void OnPositionChanged(Vector3 position)
        {
            transform.position = position;
        }
    }
}
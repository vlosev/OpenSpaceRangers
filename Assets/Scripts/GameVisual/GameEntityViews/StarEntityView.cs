using System.Linq;
using Game.Entities;
using Game.Entities.Planet;
using Game.Entities.StarSystem;
using Meta;
using UnityEngine;

namespace Game.Visual
{
    public class StarEntityView : GameEntityView<StarSystemEntity>
    {
        protected override void OnInit(StarSystemEntity entity)
        {
            Debug.Log($"create star visual object! {entity.Name}");
            // var content = GameConfig.Planets.FirstOrDefault(i => i.ID == entity.Description.PlanetId);
            // if (content != null)
            // {
            //     Debug.Log($"init view for planet '{content.Name}' / view: {content.View}");
            // }
            // else
            // {
            //     Debug.LogError($"planet content not found");
            // }
        }
    }
}
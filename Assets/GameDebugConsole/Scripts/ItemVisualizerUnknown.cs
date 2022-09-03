using Game.Entities;
using TMPro;
using UnityEngine;

namespace GameDebugConsole
{
    public class ItemVisualizerUnknown : ItemVisualizer<GameEntity>
    {
        [SerializeField] private TextMeshProUGUI nameLabel;

        protected override void OnInit(GameEntity entity)
        {
            nameLabel.text = $"{entity.EntityType}: {entity.Name}";
        }
    }
}
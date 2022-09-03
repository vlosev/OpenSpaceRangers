using Common;
using Game.Entities.StarSector;
using TMPro;
using UnityEngine;

namespace GameDebugConsole
{
    public class StarSectorItemVisualizer : ItemVisualizer<StarSectorEntity>
    {
        [SerializeField] private TextMeshProUGUI nameLabel;

        protected override void OnInit(StarSectorEntity entity)
        {
            nameLabel.text = entity.Name;
        }
    }
}
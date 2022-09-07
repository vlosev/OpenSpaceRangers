using System.Collections.Generic;
using System.Text;
using Game.Entities.StarSector;
using TMPro;
using UnityEngine;

namespace GameDebugConsole
{
    public class StarSectorItemVisualizer : ItemVisualizer<StarSectorEntity>
    {
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI contentLabel;

        private StarSectorEntity sectorEntity;
        
        private readonly StringBuilder contentBuilder = new();

        protected override void OnInit(StarSectorEntity entity)
        {
            sectorEntity = entity;
            nameLabel.text = entity.Name;
        }

        public override void Refresh()
        {
            contentBuilder.Clear();
            
            //проходимся по звездным системам и выводим их данные в консоль
            foreach (var e in sectorEntity.Stars)
            {
                contentBuilder.AppendLine($"Система '{e.Name}'");
                //TODO: добавить сюда корабли/станции, в общем все, что так или иначе находится внутри систем
            }

            contentLabel.text = contentBuilder.ToString();
        }
    }
}
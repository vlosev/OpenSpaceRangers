using System;

namespace Game.Entities.StarSector
{
    [Serializable]
    public class StarSectorDescription : GameEntityDescription
    {
        public StarSectorDescription(string name) : base(name, GameEntityType.StarSector)
        {
        }
    }
}
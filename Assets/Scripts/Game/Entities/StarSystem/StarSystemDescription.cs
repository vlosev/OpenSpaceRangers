using System;
using UnityEngine;

namespace Game.Entities.StarSystem
{
    [Serializable]
    public class StarSystemDescription : GameEntityDescription, ISerializationCallbackReceiver
    {
        [SerializeField] private string starSectorGuid;
        
        private Guid starSectorId;

        public Guid StarSectorId => starSectorId;

        public StarSystemDescription(string name, Guid starSectorId) : base(name, GameEntityType.StarSystem)
        {
            this.starSectorId = starSectorId;
        }

        public void OnBeforeSerialize()
        {
            starSectorGuid = starSectorId.ToString();
        }

        public void OnAfterDeserialize()
        {
            if (Guid.TryParse(starSectorGuid, out var guid))
                starSectorId = guid;
        }
    }
}
using System;
using Common.Serialization;
using UnityEngine;

namespace Game.Entities.StarSector
{
    [Serializable]
    public class StarSectorEntityState : GameEntityState<StarSectorDescription>
    {
        [SerializeField] private string[] starSystemsGuids;

        public Guid[] StarSystemsIds;

        public override void OnBeforeSerialize()
        {
            base.OnBeforeSerialize();
            starSystemsGuids = SerializationUtils.ConvertGuidToStringArray(StarSystemsIds);
        }

        public override void OnAfterDeserialize()
        {
            StarSystemsIds = SerializationUtils.ConvertStringToGuidArray(starSystemsGuids);
            base.OnAfterDeserialize();
        }
    }
}
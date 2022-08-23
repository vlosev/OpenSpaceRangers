using Common.FSM;
using UnityEngine;

namespace Game.Entities.Ship
{
    public class ShipDescription : EntityDescription
    {
        public readonly ShipType shipType;
        
        public ShipDescription(string name, ShipType shipType) : base(name)
        {
            this.shipType = shipType;
        }
    }
    
    public partial class ShipEntity : GameEntity<ShipDescription>         
    {
        [SerializeField] private HealthEntity healthEntity;
        
        private Fsm<ShipEntity, float> fsm;

        protected override void SafeAwake()
        {
            fsm = new Fsm<ShipEntity, float>(new Ship_Alive(this));
        }

        protected override void OnDispose()
        {
            fsm.Dispose();
        }
    }
}
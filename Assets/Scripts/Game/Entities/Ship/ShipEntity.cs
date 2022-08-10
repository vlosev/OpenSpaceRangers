using Common.FSM;
using UnityEngine;

namespace Game.Entities.Ship
{
    public partial class ShipEntity : GameEntity         
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
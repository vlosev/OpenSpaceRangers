using Common.FSM;

namespace Game.Entities.Ship
{
    public partial class ShipEntity
    {
        /// <summary>
        /// базовое состояние корабля, которое содержит все необходимые ссылки
        /// </summary>
        public abstract class Ship_BaseState : FsmState<ShipEntity, float>
        {
            protected Ship_BaseState(ShipEntity entity) : base(entity)
            {
            }
        }
    }
}
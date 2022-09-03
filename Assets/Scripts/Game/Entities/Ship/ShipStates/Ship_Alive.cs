using Common.FSM;

namespace Game.Entities.Ship
{
    public partial class ShipEntity
    {
        // public class Ship_Alive : Ship_BaseState
        // {
        //     private bool isDead;
        //     
        //     public Ship_Alive(ShipEntity entity) : base(entity)
        //     {
        //     }
        //
        //     private void OnChangeHealth(float health)
        //     {
        //         if (health <= 0f)
        //         {
        //             entity.fsm.State = new Ship_Dead(entity);
        //         }
        //     }
        //
        //     public override FsmState<ShipEntity, float> Update(float args)
        //     {
        //         return base.Update(args);
        //     }
        // }
        //
        // public class Ship_Dead : Ship_BaseState
        // {
        //     public Ship_Dead(ShipEntity entity) : base(entity)
        //     {
        //     }
        //
        //     public override FsmState<ShipEntity, float> Update(float args)
        //     {
        //         return base.Update(args);
        //     }
        // }
    }
}
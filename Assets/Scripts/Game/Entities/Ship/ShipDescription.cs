namespace Game.Entities.Ship
{
    public class ShipDescription : GameEntityDescription
    {
        public readonly ShipType shipType;
        
        public ShipDescription(string name, ShipType shipType) : base(name, GameEntityType.Ship)
        {
            this.shipType = shipType;
        }
    }
}
namespace Game.Entities.Asteroid
{
    public class AsteroidDescription : EntityDescription
    {
        public AsteroidDescription(string name) : base(name)
        {
        }
    }
    
    public class AsteroidEntity : GameEntity<AsteroidDescription>
    {
    }
}
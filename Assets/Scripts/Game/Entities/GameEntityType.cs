namespace Game.Entities
{
    /// <summary>
    /// базовый тип сущности, чтобы понимать кто вообще перед нами и потом уже различать конкретику
    /// </summary>
    public enum GameEntityType
    {
        /// <summary>
        /// космический корабль
        /// </summary>
        Ship = 0,
        
        /// <summary>
        /// космическая станция
        /// </summary>
        SpaceStation = 1,
        
        /// <summary>
        /// планета
        /// </summary>
        Planet = 2,
        
        /// <summary>
        /// астероид
        /// </summary>
        Asteroid = 3,
        
        /// <summary>
        /// снаряд или что-то другое, несущее урон кому-то или окружающим
        /// </summary>
        Projectile = 4,
        
        /// <summary>
        /// звездная система
        /// </summary>
        StarSystem = 5,

        /// <summary>
        /// звездный сектор
        /// </summary>
        StarSector = 6,
    }
}
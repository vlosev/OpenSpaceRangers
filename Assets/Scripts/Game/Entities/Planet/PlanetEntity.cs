using System;
using Common;
using Game.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Entities.Planet
{
    public class PlanetEntity : GameEntity<PlanetDescription, PlanetEntityState>, IVisibleEntity
    {
        private Vector3 direction;
        private ReactiveProperty<Vector3> position = new();
        
        private float angle;
        private float targetAngle;
        private float speed;
        
        //TODO: пробросить id вьюшки из контента
        public string View => "planet1";

        public IReadonlyReactiveProperty<Vector3> Position => position;

        public PlanetEntity(Guid guid, GameWorld world) : base(guid, world)
        {
            angle = Random.Range(0f, 360f);
            speed = Random.Range(10f, 30f);
            
            direction = new Vector3(0f, 3f, 0f);
            position.Value = Quaternion.Euler(0, 0, angle) * direction;
        }

        public override void BeforeDay()
        {
            targetAngle = angle + speed;
        }

        public override void Update(float dt)
        {
            var curAngle = Mathf.Lerp(angle, targetAngle, dt);
            
            position.Value = Quaternion.Euler(0, 0, curAngle) * direction;
        }
        
        public override void AfterDay()
        {
            angle = targetAngle;
        }
    }
}
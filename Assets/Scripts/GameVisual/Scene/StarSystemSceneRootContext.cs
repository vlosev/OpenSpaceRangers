using Game.Entities.StarSystem;
using GameSystems;

namespace Game.Visual
{
    public class StarSystemSceneRootContext
    {
        public readonly GameTimeMachine GameTimeMachine;
        public readonly StarSystemEntity StarSystemEntity;

        public StarSystemSceneRootContext(StarSystemEntity starSystemEntity, GameTimeMachine gameTimeMachine)
        {
            StarSystemEntity = starSystemEntity;
            GameTimeMachine = gameTimeMachine;
        }
    }
}
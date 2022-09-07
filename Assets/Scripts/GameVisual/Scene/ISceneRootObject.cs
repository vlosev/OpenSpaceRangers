using System;
using Game.World;

namespace Game.Visual
{
    public interface ISceneRootObject
    {
        void Init(StarSystemSceneRootContext ctx, Action onComplete);
    }
}
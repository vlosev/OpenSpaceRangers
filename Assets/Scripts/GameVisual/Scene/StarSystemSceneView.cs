using System;
using System.Collections.Generic;
using Common;
using Game.Entities;
using Game.Visual.ViewStorage;
using UnityEngine;

namespace Game.Visual
{
    public class StarSystemSceneView : NotifiableMonoBehaviour, ISceneRootObject
    {
        [SerializeField] private GameEntityViewFactory factory;

        private readonly List<GameEntityView> views = new();

        public void Init(StarSystemSceneRootContext ctx, Action onComlete)
        {
            var worldEntities = ctx.StarSystemEntity.World.Entities;
            foreach (var entity in worldEntities)
            {
                if (entity is IVisibleEntity visibleEntity)
                {
                    var view = factory.CreateView(visibleEntity);
                    views.Add(view);
                    
                    view.Init(entity);
                }
            }
            
            Debug.Log($"StarSystem scene loaded, entities: {worldEntities.Count}");

            onComlete?.Invoke();
        }

        protected override void OnDispose()
        {
            foreach (var view in views)
            {
                Destroy(view.gameObject);
            }
        }
    }
}
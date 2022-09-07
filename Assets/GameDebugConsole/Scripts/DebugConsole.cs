using System;
using System.Collections.Generic;
using Common;
using Game.Entities;
using Game.World;
using UnityEngine;

namespace GameDebugConsole
{
    public class DebugConsole : NotifiableMonoBehaviour
    {
        [Serializable]
        private class VisualizerPrefab
        {
            [field: SerializeField] public GameEntityType EntityType { get; private set; }
            [field: SerializeField] public ItemVisualizer ItemVisualizerPrefab { get; private set; }
        }
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private VisualizerPrefab[] visualizersPrefabs;
        [SerializeField] private ItemVisualizer unknownVisualizerPrefab;
        [SerializeField] private RectTransform visualizersParent;

        private GameWorld world;
        private readonly Dictionary<GameEntityType, ItemVisualizer> visualizersPrefabsCache = new();
        private readonly Dictionary<GameEntity, ItemVisualizer> visualizers = new();

        protected override void SafeAwake()
        {
            foreach (var prefabSettings in visualizersPrefabs)
            {
                visualizersPrefabsCache.Add(prefabSettings.EntityType, prefabSettings.ItemVisualizerPrefab);
            }
        }

        public void Init(GameWorld world)
        {
            this.world = world;

            world.OnBuildLinks += OnBuildLinks;
            world.OnAddEntity += OnAddEntity;
            world.OnRemoveEntity += OnRemoveEntity;
        }

        protected override void OnDispose()
        {
            if (world != null)
            {
                world.OnBuildLinks -= OnBuildLinks;
                world.OnAddEntity += OnAddEntity;
                world.OnRemoveEntity += OnRemoveEntity;
            }

            base.OnDispose();
        }

        private void OnBuildLinks()
        {
            foreach (var visualizer in visualizers.Values)
                visualizer.Refresh();
        }
        
        private void OnAddEntity(GameEntity entity)
        {
            if (visualizersPrefabsCache.TryGetValue(entity.EntityType, out var visualizerPrefab))
            {
                var visualizer = Instantiate(visualizerPrefab, visualizersParent, false);
                visualizers.Add(entity, visualizer);

                visualizer.Init(entity);
                visualizer.gameObject.SetActive(true);
            }
        }
        
        private void OnRemoveEntity(GameEntity entity)
        {
            if (visualizers.Remove(entity, out var visualizer))
            {
                Destroy(visualizer.gameObject);
            }
        }
    }
}
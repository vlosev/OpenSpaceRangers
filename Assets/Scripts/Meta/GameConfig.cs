using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Meta.Attributes;
using Meta.Scheme;
using UnityEngine;

namespace Meta
{
    [Serializable]
    public class SerializedContainer<T> : ISerializationCallbackReceiver where T : IContentItem
    {
        [SerializeField] private T[] items;

        private readonly Dictionary<int, T> dictionary = new();

        public T[] Items => items;
        public IReadOnlyDictionary<int, T> ReadOnlyDictionary => dictionary;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            foreach (var item in items)
            {
                dictionary.Add(item.ID, item);
            }
        }
    }
    
    public class GameConfig
    {
        [SchemePath("race.json")] 
        private SerializedContainer<Race> raceContainer;
        [SchemePath("planets.json")]
        private SerializedContainer<Planet> planetContainer;
        [SchemePath("stars.json")]
        private SerializedContainer<Star> starsContainer;

        private static GameConfig instance;

        public static Race[] Race => instance.raceContainer.Items;
        public static Star[] Stars => instance.starsContainer.Items;
        public static Planet[] Planets => instance.planetContainer.Items;

        private void BuildLinks()
        {
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            var configLoader = new GameConfig();

            var privateFields = configLoader.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in privateFields)
            {
                var schemePathAttribs = (SchemePathAttribute[]) field.GetCustomAttributes(typeof(SchemePathAttribute), false);
                var attr = schemePathAttribs.FirstOrDefault();
                if (attr != null)
                {
                    var path = Path.Combine(Application.streamingAssetsPath, attr.Path);
                    if (File.Exists(path))
                    {
                        var json = File.ReadAllText(path);
                        field.SetValue(configLoader, JsonUtility.FromJson(json, field.FieldType));
                    }
                    else
                    {
                        Debug.LogError($"Can't load file '{path}'");
                    }
                }
            }

            configLoader.BuildLinks();
            
            instance = configLoader;
        }
    }
}
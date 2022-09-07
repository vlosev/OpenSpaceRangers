using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Entities;
using Game.Entities.StarSector;
using Game.Entities.StarSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.World
{
    public class GameWorldSaveManager
    {
        [Serializable]
        private class SaveData : ISerializationCallbackReceiver
        {
            [SerializeField] public StarSectorEntityState[] starSectors;
            [SerializeField] public StarSystemEntityState[] starSystems;
            
            public void OnBeforeSerialize()
            {
            }

            public void OnAfterDeserialize()
            {
            }
        }
        
        /// <summary>
        /// возвращает путь до сейвов
        /// </summary>
        public string SavePath => Path.Combine(Application.persistentDataPath, "saves");

        /// <summary>
        /// сохраняет файл
        /// </summary>
        /// <param name="world"></param>
        /// <param name="fileName"></param>
        public bool SaveFile(GameWorld world, string fileName)
        {
            try
            {
                var data = new SaveData()
                {
                    //получаем все звездные сектора
                    starSectors = world.Entities.Where(i => i.EntityType == GameEntityType.StarSector).Select(i => (StarSectorEntityState) i.State).ToArray(),
                    
                    //получаем все звездные системы
                    starSystems = world.Entities.Where(i => i.EntityType == GameEntityType.StarSystem).Select(i => (StarSystemEntityState) i.State).ToArray()
                };
                
                if (Directory.Exists(SavePath) != true)
                    Directory.CreateDirectory(SavePath);
                
                try
                {
                    var path = Path.Combine(SavePath, fileName);
                    var json = JsonUtility.ToJson(data, true);
                    
                    File.WriteAllText(path, json);
                    
#if UNITY_EDITOR
                    Debug.Log(json);
#endif
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// загружает файл
        /// </summary>
        /// <param name="world"></param>
        /// <param name="fileName"></param>
        public bool LoadFile(GameWorld world, string fileName)
        {
            var path = Path.Combine(SavePath, fileName);
            if (!File.Exists(path))
                return false;

            try
            {
                var json = File.ReadAllText(path);
                var saveData = JsonUtility.FromJson<SaveData>(json);
                var states = new Dictionary<Guid, IGameEntityState>();

                void CreateEntitiesFromState<T>(IEnumerable<T> deserializationStates) where T : IGameEntityState
                {
                    foreach (var state in deserializationStates)
                    {
                        var entity = world.AddEntity(state);
                        states.Add(entity.ID, state);
                    }
                }
                
                CreateEntitiesFromState(saveData.starSectors);
                CreateEntitiesFromState(saveData.starSystems);

                world.BuildLinks(states);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
            
            return true;
        }
    }
}
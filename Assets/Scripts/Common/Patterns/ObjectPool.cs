using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    public class ObjectPoolException : Exception
    {
        public ObjectPoolException() { }
        
        public ObjectPoolException(string message) : base(message) { }

        public ObjectPoolException(string message, Exception inner) : base(message, inner) { }
    }
    
    public class ObjectPool<T> where T : Component
    {
        private class Pool
        {
            public readonly Transform PoolTransform;
            public readonly List<T> poolItems = new List<T>();

            public Pool(Transform poolTransform)
            {
                PoolTransform = poolTransform;
            }
        }
        
        //словарь пуллов по объекту, где ключ = префаб, например
        private readonly Dictionary<T, Pool> pools = new Dictionary<T, Pool>();
        //словарь пуллов по инстанс id, здесь храним только пуллы включенных инстансов, чтобы корректно вернуть в нужный
        private readonly Dictionary<int, Pool> poolsByInstances = new Dictionary<int, Pool>(); 

        public void RegisterObject(Transform poolTransform, T prefab, int poolSize)
        {
            if (prefab == null)
                throw new ObjectPoolException("Can't create pool from empty object!");

            if (pools.TryGetValue(prefab, out var pool) != true)
            {
                pools.Add(prefab, pool = new Pool(poolTransform));
                
                for (int i = 0; i < poolSize; ++i)
                {
                    var instance = Object.Instantiate(prefab, poolTransform);
                    instance.gameObject.SetActive(false);
                    pool.poolItems.Add(instance);
                }
            }
        }

        public T InstantiateFromPool(T prefab)
        {
            if (pools.TryGetValue(prefab, out var pool))
            {
                var poolItems = pool.poolItems;
                for (int i = 0; i < poolItems.Count; ++i)
                {
                    //здесь нам не обязательно проверять включен объект или нет, мы можем просто проверить, есть ли его инстанс в словаре пуллов
                    var instance = poolItems[i];
                    if (poolsByInstances.ContainsKey(instance.GetInstanceID()) != true)
                    {
                        poolsByInstances[instance.GetInstanceID()] = pool;
                        return instance;
                    }
                }
                
                Debug.LogWarning("Instances of this prefab is many! return non pooling new instance!");
            }
            else
            {
                Debug.LogWarning("This prefab isn't found in pool, return new instance!");
            }

            return Object.Instantiate(prefab);
        }

        public void Release(T instance)
        {
            if (instance != null)
            {
                var id = instance.GetInstanceID();
                if (poolsByInstances.TryGetValue(id, out var pool))
                {
                    instance.transform.SetParent(pool.PoolTransform);
                    instance.gameObject.SetActive(false);
                    poolsByInstances.Remove(id);
                }
                else
                {
                    Object.Destroy(instance.gameObject);
                }
            }
        }
    }
}
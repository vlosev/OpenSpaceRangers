using System;
using UnityEngine;

namespace Meta.Scheme
{
    [Serializable]
    public class Planet : IContentItem
    {
        [SerializeField] private int id;
        
        /// <summary>
        /// название планеты
        /// </summary>
        [SerializeField] private string name;
        
        /// <summary>
        /// заселенная или нет
        /// </summary>
        [SerializeField] private bool inhabited;
        
        /// <summary>
        /// расстояние от звезды
        /// </summary>
        [SerializeField] private int distance;

        /// <summary>
        /// раса, которой изначально заселена планета
        /// </summary>
        [SerializeField] private string race;

        /// <summary>
        /// id вьюшки планеты
        /// тут может надо как-то еще подумать, этот код не совершенен)))
        /// </summary>
        [SerializeField] private string view;

        public int ID => id;
        public string Name => name;
        public string View => view;
        public int Distance => distance;
    }
}
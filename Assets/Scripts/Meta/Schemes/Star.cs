using System;
using UnityEngine;

namespace Meta.Scheme
{
    [Serializable]
    public class Star : IContentItem
    {
        [SerializeField] private int id; //уникальный id
        [SerializeField] private string name; //имя
        [SerializeField] private string sector; //сектор
        [SerializeField] private int temperature; //температура
        [SerializeField] private int size; //размер
        [SerializeField] private int age; //возраст
        [SerializeField] private string[] planets;

        public int ID => id;
        public string Name => name;
        public string Sector => sector;
        public string[] Planets => planets;
    }
}
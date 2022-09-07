using System;
using UnityEngine;

namespace Meta.Scheme
{
    /// <summary>
    /// описание расы
    /// </summary>
    [Serializable]
    public class Race : IContentItem
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string flag;
        [SerializeField] private bool coalitionMember;

        //TODO: тут допишем разные качества, присущие этой расе

        public int ID => id;
        public string Name => name;
        public string Flag => flag;
        public bool CoalitionMember => coalitionMember;
    }
}
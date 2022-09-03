using System;
using Game.Entities.StarSystem;
using UnityEngine;

[CreateAssetMenu(order = 100, fileName = "stars_list.asset", menuName = "Create stars list")]
public class StarsList : ScriptableObject
{
    [Serializable]
    public class Star
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public StarSystemEntity Prefab { get; private set; }
        [field: SerializeField] public bool RandromDrop { get; private set; }
    }

    private Star[] stars;

    private void OnEnable()
    {
        //TODO: запилить кэш
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Ship;
using UnityEngine;

/*
 * тестовый игровой уровень, где кипит какая-то жизнь
 */
public class TestScene : MonoBehaviour
{
    private void Start()
    {
        CreateShip("Вася", ShipType.Ranger, Vector2.zero);
        CreateShip("Колян", ShipType.Ranger, new Vector2(0.5f, 0.1f));
        CreateShip("Питер", ShipType.Ranger, new Vector2(1f, 0.7f));
    }

    private void CreateShip(string name, ShipType shipType, Vector2 position)
    {
        var shipDescription = new ShipDescription(name, shipType);
    }
}

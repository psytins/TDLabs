using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret
{
    private void Awake()
    {
        GameManager.instance.BasicTurret.damage = 5; //default 25.0f
        GameManager.instance.BasicTurret.atkPerSecond = 10; //default 5.0f
        GameManager.instance.BasicTurret.range = 2; //default 3.0f
        GameManager.instance.BasicTurret.cost = 250; //default 50
    }
}

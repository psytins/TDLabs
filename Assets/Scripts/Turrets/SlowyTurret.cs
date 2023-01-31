using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowyTurret : Turret
{
    //This turret will slow enemies by 25%
    private void Awake()
    {
        GameManager.instance.SlowyTurret.damage = 5;
        GameManager.instance.SlowyTurret.atkPerSecond = 1; 
        GameManager.instance.SlowyTurret.range = 3; 
        GameManager.instance.SlowyTurret.cost = 25; 
    }
}

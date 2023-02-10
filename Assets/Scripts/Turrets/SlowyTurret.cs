using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowyTurret : Turret
{
    //This turret will slow enemies by 25%
    private void Awake()
    {
        GameManager.instance.SlowyTurret.damage = 3;
        GameManager.instance.SlowyTurret.atkPerSecond = 3; 
        GameManager.instance.SlowyTurret.range = 3; 
        GameManager.instance.SlowyTurret.cost = 400; 
    }
}

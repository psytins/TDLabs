using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public List<GameObject> enemies;
    private float spawnDelay;
    private float enemyCount;
    private float enemyTotal;
    private float timer = 0;

    private void Start()
    {
        //!! 
        ResetEnemyComponent();
        //-------------------------------
        FirstSetup();
        enemyCount = 0;
        enemyTotal = GameManager.instance.EPT;
        spawnDelay = 1f;
    } 

    private void ResetEnemyComponent(){ // !! To reset enemies variables each time I run the Game mode in unity --- Maybe don't need in future
        GameManager.instance.currentWave = 1;
        GameObject unit1 = Instantiate(enemies[0], new Vector2(-5.5f, -3.5f), Quaternion.identity);
        GameObject unit2 = Instantiate(enemies[1], new Vector2(-4.5f, -3.5f), Quaternion.identity);
        Destroy(unit1);
        Destroy(unit2);
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > spawnDelay && enemyCount != enemyTotal)
        {
            //Spawn Unit
            int enemyIndex = SpawnUnit();
            if (enemyIndex != -1) //if == -1 VAI BUGAR TUDO!
            {
                GameObject unit = Instantiate(enemies[enemyIndex], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                GameManager.instance.enemyTotalHP += unit.GetComponent<Enemy>().health;

            }
            //reset
            enemyCount++;
            timer = timer - spawnDelay;
        }

        if (GameManager.instance.EPT == 0)//end of the wave
        {
            GameManager.instance.currentWave++;
            HUDController.instance.UpdateRounds();
            //reset wave
            WaveSetup();
            GameManager.instance.startingHP = GameManager.instance.hp; //set the HP to current HP
            GameManager.instance.enemyTotalHP = 0; //Vida acumulada dos inimigos que spawnam
            GameManager.instance.enemyAccumHP = 0; //Vida dos inimigos que chegam à torre
            //local reset
            enemyCount = 0;
            enemyTotal = GameManager.instance.EPT;
            timer = 0;
        }
    }

    private int SpawnUnit()
    {
        float minionWeight = 0.0f;
        float shamaWeight = 0.0f;
        //add here
        Dictionary<int, float> indexWeight;
        Dictionary<int, float> indexWeightAccum;

        if (GameManager.instance.currentWave >= 1 && GameManager.instance.currentWave < 10)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[1], GameManager.instance.MinionWeightSpawn[10], (GameManager.instance.currentWave - 1) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[1], GameManager.instance.ShamaWeightSpawn[10], (GameManager.instance.currentWave - 1) /10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 10 && GameManager.instance.currentWave < 20)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[10], GameManager.instance.MinionWeightSpawn[20], (GameManager.instance.currentWave - 10) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[10], GameManager.instance.ShamaWeightSpawn[20], (GameManager.instance.currentWave - 10) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 20 && GameManager.instance.currentWave < 30)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[20], GameManager.instance.MinionWeightSpawn[30], (GameManager.instance.currentWave - 20) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[20], GameManager.instance.ShamaWeightSpawn[30], (GameManager.instance.currentWave - 20) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 30 && GameManager.instance.currentWave < 40)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[30], GameManager.instance.MinionWeightSpawn[40], (GameManager.instance.currentWave - 30) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[30], GameManager.instance.ShamaWeightSpawn[40], (GameManager.instance.currentWave - 30) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 40 && GameManager.instance.currentWave < 50)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[40], GameManager.instance.MinionWeightSpawn[50], (GameManager.instance.currentWave - 40) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[40], GameManager.instance.ShamaWeightSpawn[50], (GameManager.instance.currentWave - 40) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 50 && GameManager.instance.currentWave < 60)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[50], GameManager.instance.MinionWeightSpawn[60], (GameManager.instance.currentWave - 50) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[50], GameManager.instance.ShamaWeightSpawn[60], (GameManager.instance.currentWave - 50) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 60 && GameManager.instance.currentWave <= 70)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[60], GameManager.instance.MinionWeightSpawn[70], (GameManager.instance.currentWave - 60) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[60], GameManager.instance.ShamaWeightSpawn[70], (GameManager.instance.currentWave - 60) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave >= 70 && GameManager.instance.currentWave < 80)
        {
            minionWeight = Mathf.Lerp(GameManager.instance.MinionWeightSpawn[70], GameManager.instance.MinionWeightSpawn[80], (GameManager.instance.currentWave - 70) / 10f);
            shamaWeight = Mathf.Lerp(GameManager.instance.ShamaWeightSpawn[70], GameManager.instance.ShamaWeightSpawn[80], (GameManager.instance.currentWave - 70) / 10f);
            //add here

        }
        else if (GameManager.instance.currentWave == 80) // 80 rounds total
        {
            minionWeight = GameManager.instance.MinionWeightSpawn[80];
            shamaWeight = GameManager.instance.ShamaWeightSpawn[80];
            //add here
        }
        //Create a dict
        indexWeightAccum = new Dictionary<int, float>();
        indexWeight = new Dictionary<int, float>() 
        {
            {0,minionWeight },
            {1,shamaWeight }
            //add here
        };

        //Calculate cumulative probability
        int accumValue = 0;
        foreach (KeyValuePair<int, float> weight in indexWeight.OrderBy(key => key.Value))
        {
            accumValue += Mathf.CeilToInt(weight.Value * 100);
            indexWeightAccum.Add(weight.Key, accumValue);
            //Debug.Log("Index " + weight.Key + " and " + accumValue + "%");
        }
        //Compare
        int randomNumber = Random.Range(1, 101);
        foreach (KeyValuePair<int, float> weighAccum in indexWeightAccum)
        {
            if(randomNumber <= weighAccum.Value)
            {
                return weighAccum.Key;
            }

        }
        return -1;
    }

    //Only Runs the first time
    private void FirstSetup()//Before the game starts (esta função tem que correr antes dos inimigos spawnarem) 
    {
        //Spawn Init
        GameManager.instance.currentWave = 1;
        GameManager.instance.totalWaves = 80;
        //DDA Init --------------------------------------------------------
        GameManager.instance.startingHP = GameManager.instance.hp; //HP no ínicio da ronda
        GameManager.instance.enemyTotalHP = 0; //Vida acumulada dos inimigos que spawnam
        GameManager.instance.enemyAccumHP = 0; //Vida dos inimigos que chegam à torre
        //Spawn Variables
        GameManager.instance.DPG = 10; //Difficulty Point Global
        GameManager.instance.StatusPoint = 1;
        GameManager.instance.GoldPoint = 1;
        GameManager.instance.SpawnPoint = 1;
        GameManager.instance.BonusGold = 0; //Bonus Gold set to 0
        //Spawn Multipliers
        GameManager.instance.StatusMultiplier = 1;
        GameManager.instance.GoldMultiplier = 1;
        GameManager.instance.SpawnMultiplier = 1;
        //Enemy Setup
        GameManager.instance.EPT = 20; //start with 20 enemies in the first round

        //Debug Area --------------------------------------------------------------------------------------------------------------------
        // Debug.Log("--------------- Wave number " + GameManager.instance.currentWave + " ---------------------");
        // Debug.Log("Starting Health = " + GameManager.instance.startingHP);
        // Debug.Log("Enemies Health Total = " + GameManager.instance.enemyTotalHP);
        // Debug.Log("Enemies Health Acc = " + GameManager.instance.enemyAccumHP);
        // Debug.Log("Bonus Gold = " + GameManager.instance.BonusGold);
        // Debug.Log("New EPT = " + GameManager.instance.EPT);
        // Debug.Log("------------- Variables And Multipliers ------------ - ");
        // Debug.Log("Difficulty Point Global = " + GameManager.instance.DPG);
        // Debug.Log("Status Point = " + GameManager.instance.StatusPoint);
        // Debug.Log("Status Multiplier = " + GameManager.instance.StatusMultiplier);
        // Debug.Log("Spawn Point = " + GameManager.instance.SpawnPoint);
        // Debug.Log("Spawn Multiplier = " + GameManager.instance.SpawnMultiplier);
        // Debug.Log("Gold Point = " + GameManager.instance.GoldPoint);
        // Debug.Log("Gold Multiplier = " + GameManager.instance.GoldMultiplier);
        // Debug.Log("--------------- Minion Monster ---------------------");
        // Debug.Log("Minions Health = " + GameManager.instance.MinionMonster.health);
        // Debug.Log("Minions Armor = " + GameManager.instance.MinionMonster.armor);
        // Debug.Log("Minions Spawn = " + GameManager.instance.MinionMonster.spawnCount);
        // Debug.Log("--------------- Shama Monster ---------------------");
        // Debug.Log("Shama Health = " + GameManager.instance.ShamaMonster.health);
        // Debug.Log("Shama Armor = " + GameManager.instance.ShamaMonster.armor);
        // Debug.Log("Shama Spawn = " + GameManager.instance.ShamaMonster.spawnCount);
    }

    //Called in the end of each wave
    private void WaveSetup()
    {
        //Increment of Multipliers based on Players’ Lives -----------------------------------------
        int healthLoss = GameManager.instance.startingHP - GameManager.instance.hp;
        if(healthLoss == 0)
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier + 0.07f; 
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier + 0.05f; 
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier - 0.05f;
            //----
            GameManager.instance.StatusPoint = GameManager.instance.StatusPoint + 1;
            GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint + 1;
        }
        else if (healthLoss >= 1 && healthLoss <= 3)
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.10f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.08f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.07f;
            //----
            GameManager.instance.StatusPoint = GameManager.instance.StatusPoint + 0;
            GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint + 0;
        }
        else if (healthLoss >= 4 && healthLoss <= 7)
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.12f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.10f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.09f;
            //----
            GameManager.instance.StatusPoint = GameManager.instance.StatusPoint - 2;
            GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint - 2;
        }
        else if (healthLoss >= 8 && healthLoss <= 10)
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.17f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.14f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.12f;
            //----
            GameManager.instance.StatusPoint = GameManager.instance.StatusPoint - 3;
            GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint - 3;
        }
        else if (healthLoss > 10)
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.20f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.16f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.15f;
            //----
            GameManager.instance.StatusPoint = GameManager.instance.StatusPoint - 4;
            GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint - 4;
        }
        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        //Increment of Multipliers based on Enemies’ Health
        int EHR = Mathf.RoundToInt((GameManager.instance.enemyAccumHP / GameManager.instance.enemyTotalHP) * 100); //Enemies’ Health Remaining
        if (EHR == 0) // %
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier + 0.06f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier + 0.05f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier - 0.04f;
            //----
            GameManager.instance.DPG = GameManager.instance.DPG + 4;
        }
        else if (EHR < 4) // %
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.04f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.03f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.02f;
            //----
            GameManager.instance.DPG = GameManager.instance.DPG - 2;
        }
        else if (EHR < 10) // %
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.05f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.04f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.03f;
            //----
            GameManager.instance.DPG = GameManager.instance.DPG - 4;
        }
        else if (EHR < 20) // %
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.07f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.05f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.05f;
            //----
            GameManager.instance.DPG = GameManager.instance.DPG - 6;
        }
        else if (EHR >= 20 && EHR <= 100) // %
        {
            GameManager.instance.StatusMultiplier = GameManager.instance.StatusMultiplier - 0.10f;
            GameManager.instance.SpawnMultiplier = GameManager.instance.SpawnMultiplier - 0.075f;
            GameManager.instance.GoldMultiplier = GameManager.instance.GoldMultiplier + 0.075f;
            //----
            GameManager.instance.DPG = GameManager.instance.DPG - 8;
        }
        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        //Increment of Multipliers based on Gold
        ////Not implemented
        // ------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------
        //Calculation of the Formulas
        //Multipliers
        GameManager.instance.StatusPoint = GameManager.instance.StatusPoint * GameManager.instance.StatusMultiplier;
        GameManager.instance.SpawnPoint = GameManager.instance.SpawnPoint * GameManager.instance.SpawnMultiplier;
        GameManager.instance.GoldPoint = GameManager.instance.GoldPoint * GameManager.instance.GoldMultiplier;
        //Status Point Related - Every Enemy (I changed a bit)
        //Minion
        GameManager.instance.MinionMonster.health = (GameManager.instance.MinionMonster.health + GameManager.instance.StatusPoint) + (20 * (GameManager.instance.DPG / 100));
        GameManager.instance.MinionMonster.armor = (GameManager.instance.MinionMonster.armor + (GameManager.instance.StatusPoint/4)) + (20 * (GameManager.instance.DPG / 100));
        //Shama
        GameManager.instance.ShamaMonster.health = (GameManager.instance.ShamaMonster.health + GameManager.instance.StatusPoint) + (20 * (GameManager.instance.DPG / 100));
        GameManager.instance.ShamaMonster.armor = (GameManager.instance.ShamaMonster.armor + (GameManager.instance.StatusPoint/4)) + (20 * (GameManager.instance.DPG / 100));
        //...
        //Gold Point Related    
        GameManager.instance.BonusGold = Mathf.RoundToInt((50 * (GameManager.instance.DPG / 100)) + GameManager.instance.GoldPoint * 64);
        GameManager.instance.money = GameManager.instance.money + GameManager.instance.BonusGold;
        HUDController.instance.UpdateMoney(); //For HUD 
        //Spawn Point Related 
        GameManager.instance.EPT = Mathf.RoundToInt( 20 + (30 * (GameManager.instance.DPG / 100) ) + GameManager.instance.SpawnPoint);

        //Debug Area --------------------------------------------------------------------------------------------------------------------
        Debug.Log("--------------- Wave number " + GameManager.instance.currentWave + " ---------------------");
        Debug.Log("Health Loss = " + healthLoss);
        Debug.Log("Enemies Health Total = " + GameManager.instance.enemyTotalHP);
        Debug.Log("Enemies Health Acc = " + GameManager.instance.enemyAccumHP); 
        Debug.Log("Enemies Health Remaining = " + EHR + "%");
        // Debug.Log("Bonus Gold = " + GameManager.instance.BonusGold);
        Debug.Log("New EPT = " + GameManager.instance.EPT);
        Debug.Log("\n\n");
        // Debug.Log("------------- Variables And Multipliers ------------ - ");
        // Debug.Log("Difficulty Point Global = " + GameManager.instance.DPG);
        // Debug.Log("Status Point = " + GameManager.instance.StatusPoint);
        // Debug.Log("Status Multiplier = " + GameManager.instance.StatusMultiplier);
        // Debug.Log("Spawn Point = " + GameManager.instance.SpawnPoint);
        // Debug.Log("Spawn Multiplier = " + GameManager.instance.SpawnMultiplier);
        // Debug.Log("Gold Point = " + GameManager.instance.GoldPoint);
        // Debug.Log("Gold Multiplier = " + GameManager.instance.GoldMultiplier);
        // Debug.Log("--------------- Minion Monster ---------------------");
        // Debug.Log("Minions Health = " + GameManager.instance.MinionMonster.health);
        // Debug.Log("Minions Armor = " + GameManager.instance.MinionMonster.armor);
        // Debug.Log("Minions Spawn = " + GameManager.instance.MinionMonster.spawnCount);
        // Debug.Log("--------------- Shama Monster ---------------------");
        // Debug.Log("Shama Health = " + GameManager.instance.ShamaMonster.health);
        // Debug.Log("Shama Armor = " + GameManager.instance.ShamaMonster.armor);
        // Debug.Log("Shama Spawn = " + GameManager.instance.ShamaMonster.spawnCount);

    }

}

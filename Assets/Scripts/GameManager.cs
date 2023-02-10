using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject addingTurret;
    public GameObject selectedTurret;
    public GameObject cloneTurret;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        addingTurret = null;
        selectedTurret = null;
        cloneTurret = null;
        //For turrets
        BasicTurret.inserting = false;
        SlowyTurret.inserting = false;
        //add here
        LoadState();
        //DontDestroyOnLoad(gameObject);
    }
   
    private void Start()
    {
        Spawn.SetActive(false);
    }

    public void DiscardTurret(){
        //You can only discar a turret if is idle
        if(HUDController.instance.turretPanel.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text == "idle"){
            HUDController.instance.turretPanel.gameObject.SetActive(false);
            //Give 65% RAM back
            instance.money += Mathf.FloorToInt(instance.selectedTurret.GetComponent<Turret>().cost * 0.65f);
            HUDController.instance.UpdateMoney();
            //Destroy Turret
            Destroy(instance.selectedTurret);
        } 
        else {/*do nothing*/}
    }
    public void UpgradeTurret(){}

    //private void OnApplicationQuit()
    //{
    //    SaveState();
    //}

    //References
    public GameObject Spawn;
    //-------------------------
    //Wave controller
    public int currentWave;
    public int totalWaves;
    //Spawn References
    public int startingHP; //HP no ínicio da ronda
    public float enemyTotalHP; //Vida acumulada dos inimigos que spawnam
    public float enemyAccumHP; //Vida dos inimigos que chegam à torre
    //Spawn Variables
    public int DPG; //Difficulty Point Global
    public float StatusPoint;
    public float GoldPoint;
    public int BonusGold;
    public float SpawnPoint;
    public int EPT; //Enemy Point Total
    //Spawn Multipliers
    public float StatusMultiplier;
    public float GoldMultiplier;
    public float SpawnMultiplier;
    //-------------------------
    //-------------------------
    //Enemies References
    public MinionMonster MinionMonster;
    public Dictionary<int, float> MinionWeightSpawn;
    public ShamaMonster ShamaMonster;
    public Dictionary<int, float> ShamaWeightSpawn;
    //.... more monsters here
    //Turrets References
    public BasicTurret BasicTurret;
    public SlowyTurret SlowyTurret;
    //Game variables - Logic
    public int hp;
    public int money;

    //public void SaveState() 
    //{
    //    string save = "";

    //    save += hp.ToString() + "|";
    //    save += money.ToString();

    //    PlayerPrefs.SetString("SaveState", save);
    //}
    public void LoadState() 
    {
        //if (!PlayerPrefs.HasKey("SaveState"))
        //    return;

        //string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Load
        //hp = int.Parse(data[0]);
        //money = int.Parse(data[1]);
        //Game Logic Init -------------------------------------------------
        hp = 100; //temp
        money = 650; //temp

        //Set Weithgts for enemies
        MinionWeightSpawn = new Dictionary<int, float>()
        {
            { 1, 1.0f },
            { 10, 0.5f },
            { 20, 0.0f },
            { 30, 0.25f },
            { 40, 0.5f },
            { 50, 0.5f },
            { 60, 1.0f },
            { 70, 0.75f },
            { 80, 0.5f }
        };
        ShamaWeightSpawn = new Dictionary<int, float>()
        {
            { 1, 0.0f },
            { 10, 0.5f },
            { 20, 1.0f },
            { 30, 0.75f },
            { 40, 0.5f },
            { 50, 0.5f },
            { 60, 0.0f },
            { 70, 0.25f },
            { 80, 0.5f }
        };
    }


}

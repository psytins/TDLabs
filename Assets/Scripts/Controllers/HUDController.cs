using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    private void Awake()
    {
        if (HUDController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    //Text Fields
    public Text healthInput, roundsInput, totalRounds, currencyInput, basicTurretCostInput, slowyTurretCostInput;
    //information related
    public Image informationPanel,turretPanel;
    //Buttons
    public Button startButton, pauseButton, fastButton, fastFastButton, restartButton;
    //Sprites
    public Sprite pauseSprite, resumeSprite;
    private void Start()
    {
        UpdateMoney();
        UpdateHealth();
        UpdateRounds();
    }

    //Turret Selection
    public void OnTurretSelection(GameObject turret)
    {
        if (GameManager.instance.money >= turret.GetComponent<Turret>().cost)
        {
            GameManager.instance.addingTurret = turret;
        }
    }

    public void showInformation(Sprite pantentTurret)
    {
        informationPanel.GetComponent<Image>().sprite = pantentTurret;
        informationPanel.gameObject.SetActive(true);
    }
    public void closeInformation()
    {
        informationPanel.gameObject.SetActive(false);
    }
    public void StartRound()
    {
        GameManager.instance.Spawn.SetActive(true);
        roundsInput.text = GameManager.instance.currentWave.ToString();
        totalRounds.text = GameManager.instance.totalWaves.ToString();
        startButton.GetComponent<Animator>().SetBool("running", true);
        //Turn active elements on
        pauseButton.gameObject.SetActive(true);
        fastButton.gameObject.SetActive(true);
        fastFastButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void pauseGame()
    {
        if (Time.timeScale != 1f)
        {
            pauseButton.GetComponent<Image>().sprite = pauseSprite;
            Time.timeScale = 1f;
        }
        else
        {
            pauseButton.GetComponent<Image>().sprite = resumeSprite;
            Time.timeScale = 0f;
        }
    }

    public void fastGame() 
    {
        pauseButton.GetComponent<Image>().sprite = resumeSprite;
        Time.timeScale = 2f;
    }

    public void fastFastGame() 
    {
        pauseButton.GetComponent<Image>().sprite = resumeSprite;
        Time.timeScale = 4f;
    }

    //Upgrade HUD
    public void UpdateMoney()
    {
        //Money
        currencyInput.text = GameManager.instance.money.ToString();
        //Shop
        //Basic Turret
        basicTurretCostInput.text = GameManager.instance.BasicTurret.cost.ToString();
        if (GameManager.instance.BasicTurret.cost > GameManager.instance.money)
            basicTurretCostInput.color = Color.red;
        else if (GameManager.instance.BasicTurret.cost <= GameManager.instance.money)
            basicTurretCostInput.color = Color.black;
        //Slowy Turret
        slowyTurretCostInput.text = GameManager.instance.SlowyTurret.cost.ToString();
        if (GameManager.instance.SlowyTurret.cost > GameManager.instance.money)
            slowyTurretCostInput.color = Color.red;
        else if (GameManager.instance.SlowyTurret.cost <= GameManager.instance.money)
            slowyTurretCostInput.color = Color.black;
        //add here
    }
    public void UpdateRounds()
    {
        //Rounds
        roundsInput.text = GameManager.instance.currentWave.ToString();
    }
    public void UpdateHealth()
    {
        healthInput.text = GameManager.instance.hp.ToString();
        
    }
}

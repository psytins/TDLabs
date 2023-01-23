using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turret : MonoBehaviour
{
    //Turret fields
    public float damage;
    public float atkPerSecond;
    public float range;
    public int cost;
    public bool inserting;
    //---------------------
    public List<GameObject> enemyQueue;
    private GameObject currEnemy; //Current Focus
    public GameObject bullet;
    public GameObject radiusCollider;
    float sign;
    float angle;


    private float shootDelay;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Start radius collider
        radiusCollider.SetActive(true);
        //Set shootDelay
        shootDelay = 1.0f / atkPerSecond;
    }

    void Update()
    {
        //Targeting Logic        
        foreach (GameObject E in enemyQueue)
        {
            if (enemyQueue.Count != 0 && inserting == false)
            {
                //Lock Enemy --------------------
                currEnemy = E;
                //Rotate turret head --------------------
                Vector3 direction = (currEnemy.transform.position - transform.position).normalized;
                sign = (currEnemy.transform.position.x > transform.position.x) ? -1.0f : 1.0f;
                angle = Vector3.Angle(direction, transform.up);
                gameObject.transform.GetChild(0).eulerAngles = Vector3.forward * angle * sign;
                //Shoot --------------------
                timer += Time.deltaTime;
                if (timer > shootDelay)
                {
                    shootTurret();
                    timer = timer - shootDelay;
                }
                //End Cycle --------------------
                break;
            }                
        }
        
        //Stop Shooting
        if(enemyQueue.Count == 0)
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isShooting", false); //stop shooting

        //Update Information -----------------
        //Check Turret State (needs to be updated)
        if(GameManager.instance.selectedTurret != null){
            if(GameManager.instance.selectedTurret.transform.GetChild(0).GetComponent<Animator>().GetBool("isShooting"))
                HUDController.instance.turretPanel.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "shooting";
            else
                HUDController.instance.turretPanel.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "idle";
        }
    }

    private void OnMouseDown() //Turret Selecion
    {
        if(radiusCollider.GetComponent<SpriteRenderer>().enabled == true) //is selected
            TurretDeSelection();
        else if(radiusCollider.GetComponent<SpriteRenderer>().enabled == false) //is not selected
            TurretSelection();
    }

    private void TurretSelection(){
        if(GameManager.instance.selectedTurret != null){//in case other turret is selected
            GameManager.instance.selectedTurret.GetComponent<Turret>().TurretDeSelection();
        }
        
        radiusCollider.GetComponent<SpriteRenderer>().enabled = true;
    
        //Set information Panel ----------
        GameManager.instance.selectedTurret = this.gameObject;

        TextMeshProUGUI turretName = HUDController.instance.turretPanel.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        switch(this.gameObject.tag){
            case "BasicTurret": turretName.text = "Basic Turret"; break;
            case "SlowyTurret": turretName.text = "Slowy Turret"; break;
            default:break;
        }
        turretName.text += " #" + this.gameObject.transform.position.x + this.gameObject.transform.position.y;
        HUDController.instance.turretPanel.gameObject.SetActive(true);
    }
    private void TurretDeSelection(){
        radiusCollider.GetComponent<SpriteRenderer>().enabled = false;

        GameManager.instance.selectedTurret = null;

        HUDController.instance.turretPanel.gameObject.SetActive(false);
    }
    
    private void shootTurret()
    {
        //Shooting Animation
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isShooting", true);
        //shoot
        GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletClone.gameObject.GetComponent<Bullet>().Setup(currEnemy,this.gameObject,damage,sign,angle); //Shoot the bullet
    }

}

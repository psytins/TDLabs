using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnMouseDown() //Turret Selecion
    {
        radiusCollider.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnMouseExit() //Exit Turret Selection 
    {
        radiusCollider.GetComponent<SpriteRenderer>().enabled = false;
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

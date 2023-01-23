using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addTurret : MonoBehaviour
{
    private bool occupied;
    private void Start()
    {
        occupied = false;
    }

    private void Update() {
        if (GameManager.instance.addingTurret != null){
            if(Input.GetKeyDown(KeyCode.Escape)){
                //Destroy current clone turret
                Destroy(GameManager.instance.cloneTurret);
                //Cancel adding
                GameManager.instance.addingTurret.GetComponent<Turret>().inserting = false;
                GameManager.instance.cloneTurret.GetComponent<Turret>().inserting = false;
                GameManager.instance.cloneTurret = null;
                GameManager.instance.addingTurret = null;   
            }
        }
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.addingTurret != null)
        {
            if (GameManager.instance.addingTurret.GetComponent<Turret>().inserting == false)
            {
                GameManager.instance.cloneTurret = Instantiate(GameManager.instance.addingTurret, transform.position, Quaternion.identity);
                //Disable Collider in inserting mode
                GameManager.instance.cloneTurret.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.instance.addingTurret.GetComponent<Turret>().inserting = true;
                GameManager.instance.cloneTurret.GetComponent<Turret>().inserting = true;
            }
            GameManager.instance.cloneTurret.transform.position = transform.position;
        }
    }
    private void OnMouseDown()
    {
        if (occupied == false && GameManager.instance.addingTurret != null)
        {
            //Add turret
            GameManager.instance.cloneTurret.transform.position = transform.position; 
            //Money out
            GameManager.instance.money -= GameManager.instance.cloneTurret.GetComponent<Turret>().cost; 
            HUDController.instance.UpdateMoney(); 
            //Enable colliger again
            GameManager.instance.cloneTurret.GetComponent<BoxCollider2D>().enabled = true;
            GameManager.instance.addingTurret.GetComponent<Turret>().inserting = false;
            GameManager.instance.cloneTurret.GetComponent<Turret>().inserting = false;
            GameManager.instance.cloneTurret = null;
            GameManager.instance.addingTurret = null;
            occupied = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretCollider : MonoBehaviour
{
    public GameObject parentTurret;
    private CircleCollider2D circleCollider; //Temp

    // Start is called before the first frame update
    void Start()
    {
        float r = parentTurret.GetComponent<Turret>().range;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0.5f; //bruh
        //See radius
        transform.localScale = new Vector3(r, r, r) * 2; //Nota: O diametro é sempre o localScale / 2 e o raio é o floor(localSpace / 4)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            parentTurret.GetComponent<Turret>().enemyQueue.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            parentTurret.GetComponent<Turret>().enemyQueue.Remove(collision.gameObject);
        }
    }
}

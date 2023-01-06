using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject enemyFocus;
    private GameObject turret;
    private float damage;

    private float BezierOffset = 1.0f;

    private float countBezier = 0.0f;
    private Vector3 direction;
    private Vector3[] pointsBezier;
    public void Setup(GameObject focus, GameObject currTurret, float bDamage, float sign, float angle)
    {
        //Bullet Spawn Location
        
        //Bullet Direction
        this.gameObject.transform.eulerAngles = Vector3.forward * angle * sign;

        enemyFocus = focus;
        turret = currTurret;
        damage = bDamage;

        pointsBezier = new Vector3[3];
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (enemyFocus == null)
            Destroy(this.gameObject);
        else
        {
            direction = (enemyFocus.transform.position - turret.transform.position).normalized;

            //Quadratic Bezier Curve
            pointsBezier[0] = turret.transform.position;
            pointsBezier[2] = enemyFocus.transform.position;
            pointsBezier[1] = pointsBezier[0] + (pointsBezier[2] - pointsBezier[0]) / 2 + direction * BezierOffset;
            if (countBezier < 1.0f)
            {
                countBezier += 1.0f * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(pointsBezier[0], pointsBezier[1], countBezier);
                Vector3 m2 = Vector3.Lerp(pointsBezier[1], pointsBezier[2], countBezier);
                transform.position = Vector3.Lerp(m1, m2, countBezier);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().hitEnemy(damage,turret);
            Destroy(this.gameObject);
        }
    }
}

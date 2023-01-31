using UnityEngine;

public class Enemy : EnemyMovement
{
    //Enemy Fields
    public float health;
    public float armor;
    public int rpk; //reward per kill
    public float spawnCount;
    //----------------------
    //Métodos
    public void hitEnemy(float damage, GameObject enemyTurret) 
    {
        this.health -= damage;
        //Add turrets buffs on enemies
        switch (enemyTurret.tag)
        {
            case "BasicTurret": //No buffs
                break;
            case "SlowyTurret": //Freeze enemies
                this.speed = this.speed - (this.speed * 0.03f); //Slow enemy by 3% for each hit
                break;
        }
        if(health <= 0.0f)
        {
            //Retirar da Enemy Queue
            enemyTurret.GetComponent<Turret>().enemyQueue.Remove(this.gameObject);
            //Retirar enemy da game manager
            GameManager.instance.EPT -= 1;
            //Dar recompensa 
            GameManager.instance.money += this.rpk;
            HUDController.instance.UpdateMoney();
            //Inimigo morre
            Destroy(this.gameObject);
        }
    }
}

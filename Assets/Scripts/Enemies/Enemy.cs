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
    public void hitEnemy(float damage, GameObject enemyTurret) // Hit enemy
    {
        // Damage reduction - armor 
        float damageReduction = 0; //in decimal percentage
        damageReduction = (1 - (100 / (100 + this.armor))); //taken from here -> https://www.strategyzero.com/blog/2011/league-of-legends-armour-and-magic-resistance-damage-reduction/
        damage = damage - (damage * damageReduction); 
        //Apply damage to the enemy
        this.health -= damage;
        //Add turrets buffs on enemies
        switch (enemyTurret.tag)
        {
            case "BasicTurret": //No buffs
                break;
            case "SlowyTurret": //Freeze enemies
                this.speed = this.speed - (this.speed * 0.05f); //Slow enemy by 5% for each hit
                break;
        }
        if(health < 1f)
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

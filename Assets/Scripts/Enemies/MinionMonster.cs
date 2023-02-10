public class MinionMonster : Enemy
{
    private void Awake()
    {
        //this.health = 50; //default 50.0f
        //this.armor = 10; //default: 10.0f
        //this.speed = 2; //default: 2f
        //this.rpk = 2; //default: 2.0f
        //this.spawnCount = 1; //default: 1.0f
        if (GameManager.instance.currentWave == 1)
        {
            GameManager.instance.MinionMonster.health = 50; //default 50
            GameManager.instance.MinionMonster.armor = 5; //default: 5
            GameManager.instance.MinionMonster.speed = 1; //default: 1
            GameManager.instance.MinionMonster.rpk = 5; //default: 1
            GameManager.instance.MinionMonster.spawnCount = 1; //default: 1
        }
    }
}

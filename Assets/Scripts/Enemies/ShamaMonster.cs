public class ShamaMonster : Enemy
{
    private void Awake()
    {
        if (GameManager.instance.currentWave == 1)
        {
            GameManager.instance.ShamaMonster.health = 30; //default 30
            GameManager.instance.ShamaMonster.armor = 1; //default: 1
            GameManager.instance.ShamaMonster.speed = 3; //default: 3
            GameManager.instance.ShamaMonster.rpk = 8; //default: 1
            GameManager.instance.ShamaMonster.spawnCount = 1; //default: 1
        }
    }
}

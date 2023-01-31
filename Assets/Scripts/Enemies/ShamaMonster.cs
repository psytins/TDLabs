public class ShamaMonster : Enemy
{
    private void Awake()
    {
        if (GameManager.instance.currentWave == 1)
        {
            GameManager.instance.ShamaMonster.health = 25; //default 50.0f
            GameManager.instance.ShamaMonster.armor = 1; //default: 10.0f
            GameManager.instance.ShamaMonster.speed = 3; //default: 2f
            GameManager.instance.ShamaMonster.rpk = 1; //default: 2.0f
            GameManager.instance.ShamaMonster.spawnCount = 1; //default: 1.0f
        }
    }
}

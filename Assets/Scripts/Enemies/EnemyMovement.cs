using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Controll
    public bool wasInvoked; //when two turrets hit the same target at tthe same time, prevent hit function run 2 times 
    //Movement Parameter
    public float speed;

    //Movement AI
    private int direction;
    private RaycastHit2D hit;
    private RaycastHit2D hitCheck;

    // Direction: 
    // UP -> 1
    // DOWN -> -1
    // LEFT -> 2
    // RIGHT -> -2


    private void Start()
    {
        //Inicial Direction
        direction = -2;
        transform.eulerAngles = Vector3.forward * 90 * -1;

        wasInvoked = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.5f, LayerMask.GetMask("World", "Tower"));
        if (hit.collider == null)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if(hit.collider.name == "Tower")
        {
            //Apply Cummulative Enemy HP - For DDA
            GameManager.instance.enemyAccumHP += gameObject.GetComponent<Enemy>().health;
            //Retirar enemy da game manager - For DDA
            GameManager.instance.EPT -= 1;
            //aplly damage to the player
            GameManager.instance.hp -= 1;
            HUDController.instance.UpdateHealth();
            if (GameManager.instance.hp == 0) //Game ends / Game Over
                Time.timeScale = 0; //Game ends / Game Over
            //Destruir inimigo
            Destroy(gameObject);
        }
        else
        {
            //Switch direction
            direction = getDirection(direction);
            switch (direction)
            {
                case 1: transform.eulerAngles = Vector3.forward * 0; break;
                case -1: transform.eulerAngles = Vector3.forward * 180; break;
                case 2: transform.eulerAngles = Vector3.forward * 90; break;
                case -2: transform.eulerAngles = Vector3.forward * 90 * -1; break;
            }
        }
    }
    private int getDirection(int lastDirection) {
        //Test directions and return the correct new direction
        //Check left
        hitCheck = Physics2D.Raycast(transform.position, Vector2.left, 1, LayerMask.GetMask("World"));
        if (hitCheck.collider == null && lastDirection != -2) { return 2; }
        //Check right
        hitCheck = Physics2D.Raycast(transform.position, Vector2.right, 1, LayerMask.GetMask("World"));
        if (hitCheck.collider == null && lastDirection != 2) { return -2; }
        //Check up
        hitCheck = Physics2D.Raycast(transform.position, Vector2.up, 1, LayerMask.GetMask("World"));
        if (hitCheck.collider == null && lastDirection != -1) { return 1; }
        //Check down
        hitCheck = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("World"));
        if (hitCheck.collider == null && lastDirection != 1) { return -1; }
        return -2; //default 
    }
}

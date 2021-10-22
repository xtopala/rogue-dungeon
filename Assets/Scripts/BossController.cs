using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;

            // handle movement
            moveDirection = Vector2.zero;

            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                if (actions[currentAction].moveToPoint)
                {
                    moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                }
            }

            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;

            // handle shooting
            if (actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;
                    foreach (Transform t in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }   
                }
            }
        }
        else
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLength;
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;
}

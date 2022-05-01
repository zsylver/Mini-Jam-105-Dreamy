using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour
{
//---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------        
    [System.NonSerialized]
    public GameObject[] killzone;

    [System.NonSerialized]
    public TestManageMob manager;

    [System.NonSerialized]
    public GameObject Player;

    [System.NonSerialized]
    public float moveSpeed;

    [System.NonSerialized]
    public float initialSpeed;

    [System.NonSerialized]
    public float jumpHeight;

    [System.NonSerialized]
    public float initialJumpHeight;

    [System.NonSerialized]
    public List<Vector3> reversePositions = new List<Vector3>();

    [System.NonSerialized]
    public List<Vector3> forwardPositions = new List<Vector3>();

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------


    Sprite obj;
    float baseSpeed = 6;

    float baseJumpHeight = 15;
    
    float baseJumpDelay = 1;
    float jumpDelay;
    float initialJumpDelay;

    float baseJumpDuration = 0.8f;
    float jumpDuration;
    float initialJumpDuration;

    bool jumpFinish = true;

    int fiftyPercentRNGNum;
    bool fiftyPercentRNG = false;

    int giveUpChanceChecker = 0;
    int minChance = 0, maxChance = 100;

    bool isNoobSheep = false;

    float baseThinkingDelay = 1;
    float baseThinkingDuration = 1;

    float floorY = -5.916605f;

    float movingDuration, moveDurationMin = 1, moveDurationMax = 4;
    bool isMoving = true;

    float stoningDuration, stoneDurationMin = 1, stoneDurationMax = 4;
    bool isStoning = false;

//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
    public GameObject countzone;
    public SheepState currentState;
    public int ID;
    public bool end = false;

//---------------------------------------------
// PRIVATE [SF], SHOW in unity inspector
//---------------------------------------------
    [SerializeField]
    bool counted;

    [SerializeField]
    int baseGiveUpChance = 20;

    [SerializeField]
    float maxThinkingDelay;

    [SerializeField]
    float maxThinkingDuration;

    [SerializeField]
    float maxMoveSpeed;

    [SerializeField]
    float maxJumpHeight;

    [SerializeField]
    float maxJumpDelay;

    [SerializeField]
    float maxJumpDuration;

//---------------------------------------------
// FUNCTIONS
//---------------------------------------------
    void Start()
    {
        obj = GetComponent<Sprite>();
        counted = false;

        moveSpeed = Random.Range(baseSpeed, maxMoveSpeed + 1);
        initialSpeed = moveSpeed;

        jumpHeight = Random.Range(baseJumpHeight, maxJumpHeight + 1);
        initialJumpHeight = jumpHeight;

        jumpDelay = Random.Range(baseJumpDelay, maxJumpDelay + 1);
        initialJumpDelay = jumpDelay;

        jumpDuration = Random.Range(baseJumpDuration, maxJumpDuration + 1);
        initialJumpDuration = jumpDuration;

        fiftyPercentRNGNum = Random.Range(1, 2 + 1);
        if (fiftyPercentRNGNum == 1)
            fiftyPercentRNG = false;
        else if (fiftyPercentRNGNum == 2)
            fiftyPercentRNG = true;

        giveUpChanceChecker = Random.Range(minChance, maxChance + 1);
        if (giveUpChanceChecker <= baseGiveUpChance)
        {
            isNoobSheep = true;
            baseThinkingDelay = Random.Range(baseThinkingDelay, maxThinkingDelay + 1);
            baseThinkingDuration = Random.Range(baseThinkingDuration, maxThinkingDuration + 1);
        }

        movingDuration = Random.Range(moveDurationMin, moveDurationMax);    // 1 to 3 seconds
        stoningDuration = Random.Range(stoneDurationMin, stoneDurationMax); // 1 to 3 seconds
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (end) // wander AI
        {
            if (fiftyPercentRNG)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

                if (isMoving == true) {
                    movingDuration -= Time.fixedDeltaTime;
                    transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
                    if (movingDuration <= 0)
                    {
                        isMoving = false;
                        isStoning = true;
                        stoningDuration = Random.Range(stoneDurationMin, stoneDurationMax); // 1 to 3 seconds
                    }
                }                
                else if (isStoning == true) {
                    stoningDuration -= Time.fixedDeltaTime;
                    if (stoningDuration <= 0)
                    {
                        isStoning = false;
                        isMoving = true;
                        movingDuration = Random.Range(moveDurationMin, moveDurationMax);

                        fiftyPercentRNGNum = Random.Range(1, 2 + 1);
                        if (fiftyPercentRNGNum == 1)
                            fiftyPercentRNG = false;
                        else if (fiftyPercentRNGNum == 2)
                        {
                            fiftyPercentRNG = true;
                        }
                    }                
                }
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

                if (isMoving == true)
                {
                    movingDuration -= Time.fixedDeltaTime;
                    transform.Translate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
                    if (movingDuration <= 0)
                    {
                        isMoving = false;
                        isStoning = true;
                        stoningDuration = Random.Range(2, stoneDurationMax); // 1 to 3 seconds
                    }
                }
                else if (isStoning == true)
                {
                    stoningDuration -= Time.fixedDeltaTime;
                    if (stoningDuration <= 0)
                    {
                        isStoning = false;
                        isMoving = true;
                        movingDuration = Random.Range(2, moveDurationMax);

                        fiftyPercentRNGNum = Random.Range(1, 2 + 1);
                        if (fiftyPercentRNGNum == 1)
                            fiftyPercentRNG = false;
                        else if (fiftyPercentRNGNum == 2)
                        {
                            fiftyPercentRNG = true;
                        }
                    }
                }
            }
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            // if is a balloon animal
            if (this.gameObject.transform.Find("balloon") != null && this.gameObject.transform.Find("balloon").gameObject.transform.CompareTag("flying"))
            {
                transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else // if not balloon animal
            {
                if (isNoobSheep) // give up animal
                {
                    if (baseThinkingDelay >= 0)
                    {
                        baseThinkingDelay -= Time.fixedDeltaTime;
                        transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
                    }
                    else if (baseThinkingDuration >= 0)
                        baseThinkingDuration -= Time.fixedDeltaTime;
                    else
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        transform.Translate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
                    }
                }
                else // jumping animal
                {
                    if (jumpDelay >= 0 && jumpFinish == true)
                    {
                        transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
                        jumpDelay -= Time.fixedDeltaTime;
                        if (jumpDelay <= 0)
                        {
                            jumpFinish = false;
                            jumpDuration = initialJumpDuration;
                        }
                    }
                    else if (jumpDelay <= 0 && (jumpDuration >= 0 || jumpFinish == false))
                    {
                        jumpDuration -= Time.fixedDeltaTime;
                        if (jumpDuration <= 0 && this.gameObject.transform.position.y <= floorY)
                        {
                            jumpFinish = true;
                            jumpDelay = initialJumpDelay;
                        }
                        transform.Translate(moveSpeed * Time.fixedDeltaTime, jumpHeight * Time.fixedDeltaTime, 0);
                    }
                    else
                    {
                        transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
                    }
                }
            }
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!end)
        {
            //Kill on collide w/ killzone
            for (int i = 0; i < killzone.Length; i++)
            {
                if (collision.gameObject == killzone[i])
                {
                    manager.NumberOfMobsInPlay--;
                    manager.mobArray.Remove(this.gameObject);
                    Destroy(gameObject);
                }
            }

            //count
            if (!counted && collision.gameObject == countzone)
            {
                manager.endMob.Add(ID);
                counted = true;
                if (this.gameObject.CompareTag("Sheep"))
                {
                    manager.AddCount();
                }
            }
        }

/*        else if (counted && collision.gameObject == countzone && this.gameObject.CompareTag("Sheep"))
        {
            manager.DecCount();
            counted = false;
        }*/
    }
}

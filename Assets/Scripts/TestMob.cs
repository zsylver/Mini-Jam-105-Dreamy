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

//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    Sprite obj;
    float baseSpeed = 6;

    float baseJumpHeight = 15;
    float jumpHeight;

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
        
//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
    public GameObject countzone;
    public SheepState currentState;
    
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.tag == "flying")
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
        }
        else
        {
            if (isNoobSheep)
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
            else
            {
                if (jumpDelay >= 0 && jumpFinish == true)
                {
                    jumpDelay -= Time.fixedDeltaTime;
                    if (jumpDelay <= 0)
                    {
                        jumpFinish = false;
                        jumpDuration = initialJumpDuration;
                    }
                }

                if (jumpDelay <= 0 && jumpDuration >= 0 && jumpFinish == false)
                {
                    // if this 50% rng is false, animal constantly bounces aft jumping
                    // else, it got delay between jumps
                    if (fiftyPercentRNG == true)
                    {
                        jumpDuration -= Time.fixedDeltaTime;
                        if (jumpDuration <= 0)
                        {
                            jumpFinish = true;
                            jumpDelay = initialJumpDelay;
                        }
                    }

                    transform.Translate(moveSpeed * Time.fixedDeltaTime, jumpHeight * Time.fixedDeltaTime, 0);
                }
                else
                    transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kill on collide w/ killzone
        for(int i = 0; i < killzone.Length; i++)
        {
            if (collision.gameObject == killzone[i])
            {
                manager.NumberOfMobsInPlay--;
                Destroy(gameObject);
            }
        }

        //count
        if (!counted && collision.gameObject == countzone)
        {
            manager.AddCount();
            counted = true;
        }
        else if (counted && collision.gameObject == countzone)
        {
            manager.DecCount();
            counted = false;
        }
    }
}

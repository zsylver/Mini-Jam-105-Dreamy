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

    float baseJumpDuration = 1;
    float jumpDuration;

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

        moveSpeed = Random.Range(baseSpeed, maxMoveSpeed);
        initialSpeed = moveSpeed;

        jumpHeight = Random.Range(baseJumpHeight, maxJumpHeight);
        jumpDelay = Random.Range(baseJumpDelay, maxJumpDelay);
        jumpDuration = Random.Range(baseJumpDuration, maxJumpDuration);

        giveUpChanceChecker = Random.Range(minChance, maxChance);
        if (giveUpChanceChecker <= baseGiveUpChance)
        {
            isNoobSheep = true;
            baseThinkingDelay = Random.Range(baseThinkingDelay, maxThinkingDelay);
            baseThinkingDuration = Random.Range(baseThinkingDuration, maxThinkingDuration);
        }               
    }

    // Update is called once per frame
    void FixedUpdate()
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
            if (jumpDelay >= 0)
                jumpDelay -= Time.fixedDeltaTime;

            if (jumpDelay <= 0 && jumpDuration >= 0)
                transform.Translate(moveSpeed * Time.fixedDeltaTime, jumpHeight * Time.fixedDeltaTime, 0);
            else
                transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
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

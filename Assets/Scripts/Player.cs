using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //---------------------------------------------
    // PUBLIC [S.NS], NOT in unity inspector         
    //---------------------------------------------

    //---------------------------------------------
    // PRIVATE, NOT in unity inspector
    //---------------------------------------------

    //---------------------------------------------
    // PUBLIC, SHOW in unity inspector
    //---------------------------------------------
    public float GameTimePassed;

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    TMPro.TMP_Text playercounter;

    [SerializeField]
    TMPro.TMP_Text endtext;

    [SerializeField]
    int playercount;

    [SerializeField]
    float GameDuration;

    [SerializeField]
    float GameDurationMin = 15;

    [SerializeField]
    float GameDurationMax = 60;


    [SerializeField]
    public GameObject manager;

    //---------------------------------------------
    // FUNCTIONS
    //---------------------------------------------  
    void Start()
    {
        playercount = 0;
        GameDuration = Random.Range(GameDurationMin, GameDurationMax);


        GameTimePassed = GameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (GameTimePassed > 0)
        {
            GameTimePassed -= Time.deltaTime;
        }
        else if(manager.GetComponent<TestManageMob>().NumberOfMobsInPlay == 0)
        {
            manager.GetComponent<TestManageMob>().DelKillZone();
        }
        if (manager.GetComponent<TestManageMob>().NumberOfMobsInPlay != 0)
        {
            playercounter.text = playercount.ToString();
        }

    }

    public void AddPlayerCount()
    {
        playercount++;
    }

    public int GetPlayerCount()
    {
        return playercount;
    }
    public void DecPlayeCount()
    {
        if (playercount > 0)
        {
            playercount--;
        }
    }
}

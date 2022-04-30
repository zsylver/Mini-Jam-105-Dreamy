using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmanager : MonoBehaviour
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
    int randomIndex = 0;

    int spawnZoneIndex = 0;

    float timer;
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    float spawntime = 3;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] effectprefab;

    [SerializeField]
    GameObject[] spawnzone;

    //---------------------------------------------
    // FUNCTIONS
    //---------------------------------------------   
    void Start()
    {
        timer = spawntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPause())
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //spawn game time effects
                if (Player.GetComponent<Player>().GameTimePassed > 0)
                {

                }
                else//spawn non game time effects
                {
                    spawnZoneIndex = Random.Range(0, spawnzone.Length);
                    randomIndex = Random.Range(0, effectprefab.Length);
                    GameObject mob;
                    mob = Instantiate(effectprefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                    mob.transform.Translate(-mob.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 0);
                    timer = Random.Range(spawntime - 2, spawntime + 2);
                }

            }
        }
            
    }
}

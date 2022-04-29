using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManageMob : MonoBehaviour
{
//---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------
//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    float timer;

    int randomIndex = 0,
        minSizeOfMobPrefabArray = 0,
        maxSizeOfMobPrefabArray = 0;

//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
    public int NumberOfMobsInPlay = 0;    
    public List<GameObject> mobArray = new List<GameObject>();

//---------------------------------------------
// PRIVATE [SF], SHOW in unity inspector
//---------------------------------------------
    [SerializeField]
    TMPro.TMP_Text counter;
    int count;

    [SerializeField]
    float spawntime = 5;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] mobprefab;

    [SerializeField]
    GameObject[] spawnzone;

    [SerializeField]
    GameObject[] killzone;

    [SerializeField]
    GameObject countzone;
    
//---------------------------------------------
// FUNCTIONS
//---------------------------------------------      
    void Start()
    {
        timer = spawntime;
        count = 0;

        for (int i = 0; i < mobprefab.Length; ++i)
        {
            ++maxSizeOfMobPrefabArray;
        }        
    }

    void Update()
    {
        if (Player.GetComponent<Player>().GameTimePassed > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {

                randomIndex = Random.Range(minSizeOfMobPrefabArray, maxSizeOfMobPrefabArray);

                //Spawn at spawn zone
                int temp = Random.Range(1, spawnzone.Length - 1);
                for (int i = 0; i < temp; i++)
                {
                    GameObject mob = Instantiate(mobprefab[randomIndex], spawnzone[Random.Range(0, spawnzone.Length - 1)].transform.position, Quaternion.identity);
                    mob.GetComponent<TestMob>().killzone = killzone;
                    mob.GetComponent<TestMob>().manager = this;
                    mob.GetComponent<TestMob>().countzone = countzone;
                    mob.GetComponent<TestMob>().Player = Player;
                    //mob.GetComponent<TestMob>().movespeed  = mob.GetComponent<TestMob>().initialSpeed = Random.Range(6, 9);
                    mobArray.Add(mob);
                    NumberOfMobsInPlay++;
                }
                timer = Random.Range(spawntime - 3, spawntime + 3);
            }
        }
        counter.text = count.ToString();
    }

    public void AddCount()
    {
        count++;
    }
    public void DecCount()
    {
        if(count > 0)
        {
            count--;
        }
    }
}

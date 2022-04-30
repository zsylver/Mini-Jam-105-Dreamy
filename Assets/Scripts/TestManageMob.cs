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
        maxSizeOfMobPrefabArray = 0,
        minSizeOfFlyingMobPrefabArray = 0,
        maxSizeOfFlyingMobPrefabArray = 0;

    int spawnZoneIndex = 0;

    int spawnZoneChecker = 0;
    int spawnZoneMinChance = 0, spawnZoneMaxChance = 100;
    int spawnNumber = 0;
//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
    public int NumberOfMobsInPlay = 0;    
    public List<GameObject> mobArray = new List<GameObject>();
    public List<int> endMob = new List<int>();
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    TMPro.TMP_Text counter;
    int count;

    [SerializeField]
    int baseSpawnZoneCheckerChance = 25;

    [SerializeField]
    float spawntime = 5;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] mobprefab;

    [SerializeField]
    GameObject[] flyingMobPrefab;


    [SerializeField]
    GameObject endspawn;

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

        for (int i = 0; i < flyingMobPrefab.Length; ++i)
        {
            ++maxSizeOfFlyingMobPrefabArray;
        }        
    }

    void Update()
    {
        if (Player.GetComponent<Player>().GameTimePassed > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //choosing a random amount of animal to spawn
                spawnNumber = Random.Range(1, spawnzone.Length);
                for(int i = 0; i < spawnNumber; i++)
                {
                    spawnZoneChecker = Random.Range(spawnZoneMinChance, spawnZoneMaxChance + 1);
                    if (spawnZoneChecker <= baseSpawnZoneCheckerChance)
                    {
                        //Spawn at spawn zone  
                        spawnZoneIndex = Random.Range(1, spawnzone.Length);
                    }
                    else
                        //Spawn at spawn zone  
                        spawnZoneIndex = 0;

                    GameObject mob;
                    if (spawnZoneIndex == 0) // floor spawnZone
                    {
                        // choosing a random animal to spawn
                        randomIndex = Random.Range(minSizeOfMobPrefabArray, maxSizeOfMobPrefabArray);
                        mob = Instantiate(mobprefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                        mob.GetComponent<TestMob>().ID = randomIndex;
                    }
                    else // flying spawnZone         
                    {
                        // choosing a random flying animal to spawn
                        randomIndex = Random.Range(minSizeOfFlyingMobPrefabArray, maxSizeOfFlyingMobPrefabArray);
                        mob = Instantiate(flyingMobPrefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                        mob.GetComponent<TestMob>().ID = mobprefab.Length + randomIndex + 1;
                    }

                    mob.GetComponent<TestMob>().killzone = killzone;
                    mob.GetComponent<TestMob>().manager = this;
                    mob.GetComponent<TestMob>().countzone = countzone;
                    mob.GetComponent<TestMob>().Player = Player;
                    mobArray.Add(mob);
                    NumberOfMobsInPlay++;
                }
           
                timer = Random.Range(spawntime, spawntime + 3);
            }
        }
        else if(NumberOfMobsInPlay == 0)
        {
            countzone.GetComponent<Collider2D>().isTrigger = false;
            for (int i = 0; i < endMob.Count; i++)
            {
                GameObject mob;
                if (endMob[i] > mobprefab.Length)
                {
                    mob = Instantiate(flyingMobPrefab[endMob[i] - mobprefab.Length -1], endspawn.transform.position, Quaternion.identity);
                    mob.GetComponent<TestMob>().manager = this;
                    mob.GetComponent<TestMob>().end = true;
                }
                else
                {
                    mob = Instantiate(mobprefab[endMob[i]], endspawn.transform.position, Quaternion.identity);
                    mob.GetComponent<TestMob>().manager = this;
                    mob.GetComponent<TestMob>().end = true;
                }
                endMob.RemoveAt(i);
            }

        }
        counter.text = count.ToString();
    }
    public void DelKillZone()
    {
        for (int i = 0; i < killzone.Length; ++i)
        {
            Destroy(killzone[i]);
        }
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

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

    public List<GameObject> effectArray = new List<GameObject>();
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

    [SerializeField]
    GameObject[] killzone;

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

            if (timer <= 0 && GetComponent<TimeReverse>().reverseTime == true)
            {
                spawnZoneIndex = Random.Range(0, spawnzone.Length);
                randomIndex = Random.Range(0, effectprefab.Length);
                GameObject mob;
                mob = Instantiate(effectprefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                mob.transform.Translate(-mob.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 0);
                timer = Random.Range(spawntime, spawntime + 3);

                mob.GetComponent<TestCloud>().killzone = killzone;
                mob.GetComponent<TestCloud>().manager = this;
                effectArray.Add(mob);
            }
        }
    }
}

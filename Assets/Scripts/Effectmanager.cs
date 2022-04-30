using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmanager : MonoBehaviour
{
    // Start is called before the first frame update

    int randomIndex = 0;

    int spawnZoneIndex = 0;

    float timer;

    [SerializeField]
    float spawntime = 5;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] effectprefab;

    [SerializeField]
    GameObject[] spawnzone;
    void Start()
    {
        timer = spawntime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (Player.GetComponent<Player>().GameTimePassed > 0)
            {
            }
            spawnZoneIndex = Random.Range(0, spawnzone.Length);
            randomIndex = Random.Range(0, effectprefab.Length);
            GameObject mob;
            mob = Instantiate(effectprefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
            mob.transform.Translate(-mob.GetComponent<SpriteRenderer>().bounds.size.x/2 , 0, 0);
            timer = Random.Range(spawntime, spawntime + 3);
        }
    }
}

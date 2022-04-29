using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManageMob : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text counter;
    int count;

    [SerializeField]
    float spawntime = 5;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject mobprefab;

    [SerializeField]
    GameObject[] spawnzone;

    [SerializeField]
    GameObject[] killzone;

    [SerializeField]
    GameObject countzone;

    [SerializeField]
    public int NumberOfMobsInPlay = 0;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawntime;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Player>().GameTimePassed > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //Spawn at spawn zone
                int temp = Random.Range(1, spawnzone.Length - 1);
                for (int i = 0; i < temp; i++)
                {
                    GameObject mob = Instantiate(mobprefab, spawnzone[Random.Range(0, spawnzone.Length - 1)].transform.position, Quaternion.identity);
                    mob.GetComponent<TestMob>().killzone = killzone;
                    mob.GetComponent<TestMob>().manager = this;
                    mob.GetComponent<TestMob>().countzone = countzone;
                    mob.GetComponent<TestMob>().Player = Player;
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

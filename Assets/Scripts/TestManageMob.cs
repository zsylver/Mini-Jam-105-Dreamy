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
    GameObject mobprefab;

    [SerializeField]
    GameObject[] spawnzone;

    [SerializeField]
    GameObject[] killzone;

    [SerializeField]
    GameObject countzone;

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
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            //Spawn at spawn zone
            GameObject mob = Instantiate(mobprefab, spawnzone[Random.Range(0, spawnzone.Length-1)].transform.position, Quaternion.identity);
            mob.GetComponent<TestMob>().killzone = killzone;
            mob.GetComponent<TestMob>().manager = this;
            mob.GetComponent<TestMob>().countzone = countzone;
            timer = Random.Range(spawntime - 3, spawntime + 3);
        }

        counter.text = count.ToString();
    }

    public void AddCount()
    {
        count++;
    }
}

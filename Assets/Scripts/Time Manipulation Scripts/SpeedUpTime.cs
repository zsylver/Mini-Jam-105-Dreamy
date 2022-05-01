using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpTime : MonoBehaviour
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

    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    public bool speedUpTime;

    [SerializeField]
    float speedAddition;

    [SerializeField]
    float highestSpeed;

    [SerializeField]
    int minDuration;

    [SerializeField]
    int maxDuration;

    private float timerSpeedTime;
    private float durationSpeedTime;

    private bool bufferSpeedTime;

    //public List<GameObject> mobArray = new List<GameObject>();

    // Start is called before the first frame update
    //---------------------------------------------
    // FUNCTIONS
    //---------------------------------------------  
    void Start()
    {
        timerSpeedTime = 0.0f;
        bufferSpeedTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if GameObject is destroyed, remove the list that used to contain the GameObject
        for (int h = 0; h < GetComponent<TestManageMob>().mobArray.Count; ++h)
        {
            if (GetComponent<TestManageMob>().mobArray[h] == null)
            {
                GetComponent<TestManageMob>().mobArray.Remove(GetComponent<TestManageMob>().mobArray[h]);
            }
        }

        if (speedUpTime)
        {
            timerSpeedTime += Time.fixedDeltaTime;

            if (bufferSpeedTime)
            {
                durationSpeedTime = Random.Range(minDuration, maxDuration + 1);
                bufferSpeedTime = false;
            }

            for (int i = 0; i < GetComponent<TestManageMob>().mobArray.Count; ++i)
            {
                if (GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed < highestSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed += speedAddition * Time.fixedDeltaTime;
                }
            }
            for (int g = 0; g < GetComponent<Effectmanager>().effectArray.Count; ++g)
            {
                GetComponent<Effectmanager>().effectArray[g].GetComponent<TestCloud>().panspeed = 9;
            }
        }
        else
        {
            for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
            {
                if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed > GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().initialSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed += -speedAddition * Time.fixedDeltaTime;
                }
            }
            for (int g = 0; g < GetComponent<Effectmanager>().effectArray.Count; ++g)
            {
                GetComponent<Effectmanager>().effectArray[g].GetComponent<TestCloud>().panspeed = 6;
            }
        }

        if (timerSpeedTime > durationSpeedTime)
        {
            speedUpTime = false;
            timerSpeedTime = 0.0f;
            bufferSpeedTime = true;
        }
    }
}

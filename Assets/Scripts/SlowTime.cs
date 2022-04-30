using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
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
    public bool slowTime;

    [SerializeField]
    float speedReduction;

    [SerializeField]
    float lowestSpeed;

    [SerializeField]
    int minDuration;

    [SerializeField]
    int maxDuration;

    private float timerSlowTime;
    private float durationSlowTime;

    private bool bufferSlowTime;

    // Start is called before the first frame update
    //---------------------------------------------
    // FUNCTIONS
    //---------------------------------------------  
    void Start()
    {
        timerSlowTime = 0.0f;
        bufferSlowTime = true;
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

        if (slowTime)
        {
            timerSlowTime += Time.fixedDeltaTime;

            if (bufferSlowTime)
            {
                durationSlowTime = Random.Range(minDuration, maxDuration + 1);
                bufferSlowTime = false;
            }

            for (int i = 0; i < GetComponent<TestManageMob>().mobArray.Count; ++i)
            {
                if (GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed > lowestSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed += -speedReduction * Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
            {
                if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed < GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().initialSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed += speedReduction * Time.fixedDeltaTime;
                }
            }
        }

        if (timerSlowTime > durationSlowTime)
        {
            slowTime = false;
            timerSlowTime = 0.0f;
            bufferSlowTime = true;
        }
    }
}

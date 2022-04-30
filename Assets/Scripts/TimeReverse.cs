using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour
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
    public bool reverseTime;

    [SerializeField]
    float reverseSpeed;

    [SerializeField]
    int minDuration;

    [SerializeField]
    int maxDuration;

    private float timerReverseTime;
    private float durationReverseTime;

    private bool bufferReverseTime;

    //---------------------------------------------
    // FUNCTIONS
    //---------------------------------------------  
    void Start()
    {
        timerReverseTime = 0.0f;
        bufferReverseTime = true;
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

        if (reverseTime)
        {
            timerReverseTime += Time.fixedDeltaTime;

            if (bufferReverseTime)
            {
                durationReverseTime = Random.Range(minDuration, maxDuration + 1);
                bufferReverseTime = false;
            }

            for (int i = 0; i < GetComponent<TestManageMob>().mobArray.Count; ++i)
            {
                if (GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed > -GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().initialSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().moveSpeed += -reverseSpeed * Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
            {
                if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed < GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().initialSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().moveSpeed += reverseSpeed * Time.fixedDeltaTime;
                }
            }
        }

        if (timerReverseTime > durationReverseTime)
        {
            reverseTime = false;
            timerReverseTime = 0.0f;
            bufferReverseTime = true;
        }
    }
}

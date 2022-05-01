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
    //private bool forwardTime;

    private float gravity;

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
        }
    }

    void FixedUpdate()
    {
        /*if (forwardTime)
            Forward();*/
        if (reverseTime)
            Rewind();
        else
            Record();
    }

    void Record() 
    {
        for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g) 
        {
            GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().reversePositions.Insert(0, GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().transform.position);
        }
    }

    void Rewind()
    {
        for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
        {
            if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().reversePositions.Count > 0) 
            {
                GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().transform.position = GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().reversePositions[0];
                //GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().forwardPositions.Insert(0, GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().reversePositions[0]);
                GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().reversePositions.RemoveAt(0);
            }

            if (timerReverseTime > durationReverseTime)
            {
                reverseTime = false;
                //forwardTime = true;
                timerReverseTime = 0.0f;
                bufferReverseTime = true;
            }
        }
    }

    /*void Forward()
    {
        for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
        {
            if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().forwardPositions.Count > 0)
            {
                GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().transform.position = GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().forwardPositions[0];
                GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().forwardPositions.RemoveAt(0);
            }
        }

        if (GetComponent<TestManageMob>().mobArray[GetComponent<TestManageMob>().mobArray.Count - 1].GetComponent<TestMob>().forwardPositions.Count <= 0)
            forwardTime = false;
    }*/
}

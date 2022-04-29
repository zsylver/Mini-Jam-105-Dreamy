using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpTime : MonoBehaviour
{
    [SerializeField]
    bool speedUpTime;

    [SerializeField]
    float speedAddition;

    [SerializeField]
    float highestSpeed;

    //public List<GameObject> mobArray = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

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
            for (int i = 0; i < GetComponent<TestManageMob>().mobArray.Count; ++i)
            {
                if (GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().movespeed < highestSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[i].GetComponent<TestMob>().movespeed += speedAddition * Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            for (int g = 0; g < GetComponent<TestManageMob>().mobArray.Count; ++g)
            {
                if (GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().movespeed > GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().initialSpeed)
                {
                    GetComponent<TestManageMob>().mobArray[g].GetComponent<TestMob>().movespeed += -speedAddition * Time.fixedDeltaTime;
                }
            }
        }
    }
}

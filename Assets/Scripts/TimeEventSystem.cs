using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventSystem : MonoBehaviour
{
    private float timer, bufferEvent;

    [SerializeField]
    int MinBufferRange;

    [SerializeField]
    int MaxBufferRange;

    private bool pickBuffer;
    private int chosenEvent;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        pickBuffer = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;
        if (pickBuffer) 
        {
            bufferEvent = Random.Range(MinBufferRange, MaxBufferRange + 1);
            pickBuffer = false;
        }
            
        if (timer > bufferEvent) 
        {
            eventPicker();
            timer = 0.0f;
            pickBuffer = true;
        }
    }

    private void eventPicker() 
    {
        chosenEvent = Random.Range(1, 4);
        switch (chosenEvent) 
        {
            case 1:
                GetComponent<TimeReverse>().reverseTime = true;
                break;
            case 2:
                GetComponent<SlowTime>().slowTime = true;
                break;
            case 3:
                GetComponent<SpeedUpTime>().speedUpTime = true;
                break;
            default:
                break;
        }
    }
}

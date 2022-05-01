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

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject fastforward;

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
        if (Player.GetComponent<Player>().GameTimePassed > 0)
        {
            timer += Time.fixedDeltaTime;
            if (GetComponent<TimeReverse>().reverseTime == false && GetComponent<SpeedUpTime>().speedUpTime == false)
            {
                fastforward.SetActive(false);
            }
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
    }

    private void eventPicker() 
    {
        chosenEvent = Random.Range(1, 3);
        switch (chosenEvent) 
        {
            case 1:
                GetComponent<TimeReverse>().reverseTime = true;
                fastforward.SetActive(true);
                fastforward.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case 2:
                GetComponent<SpeedUpTime>().speedUpTime = true;
                fastforward.GetComponent<SpriteRenderer>().flipX = false;
                fastforward.SetActive(true);
                break;
            default:
                break;
        }
    }
}

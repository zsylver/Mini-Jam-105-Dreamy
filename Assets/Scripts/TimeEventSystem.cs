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

    [SerializeField]
    AudioSource fastFwdSfx;

    [SerializeField]
    AudioSource reverseSfx;

    [SerializeField]
    GameObject panel;

    private bool fastFwdSfxIsPlaying, reverseSfxIsPlaying;

    private bool pickBuffer;
    private int chosenEvent;

    private int eventCount, eventCountMax;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        pickBuffer = true;
        eventCountMax = Random.Range(1, 5); // 1 to 4
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Player>().GameTimePassed > 0 && eventCount < eventCountMax && panel.activeSelf == false)
        {

            if (GetComponent<TimeReverse>().reverseTime == false && GetComponent<SpeedUpTime>().speedUpTime == false)
            {
                timer += Time.fixedDeltaTime;
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

        if (eventCount > eventCountMax || (GetComponent<TimeReverse>().reverseTime == false && GetComponent<SpeedUpTime>().speedUpTime == false))
            fastforward.SetActive(false);

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
                reverseSfx.PlayOneShot(reverseSfx.clip);
                reverseSfxIsPlaying = true;
                ++eventCount;
                break;
            case 2:
                GetComponent<SpeedUpTime>().speedUpTime = true;
                fastforward.GetComponent<SpriteRenderer>().flipX = false;
                fastforward.SetActive(true);
                fastFwdSfx.PlayOneShot(fastFwdSfx.clip);
                fastFwdSfxIsPlaying = true;
                ++eventCount;
                break;
            default:
                break;
        }
    }
}

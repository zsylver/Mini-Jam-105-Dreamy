using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TMPro.TMP_Text playercounter;

    [SerializeField]
    TMPro.TMP_Text endtext;

    [SerializeField]
    int playercount;

    [SerializeField]
    float GameDuration;

    [SerializeField]
    float GameDurationMin = 15;

    [SerializeField]
    float GameDurationMax = 60;

    public float GameTimePassed;

    void Start()
    {
        playercount = 0;
        GameDuration = Random.Range(GameDurationMin, GameDurationMax);
        GameTimePassed = GameDuration;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameTimePassed > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                playercount++;
                playercounter.text = playercount.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (playercount > 0)
                {
                    playercount--;
                }
                playercounter.text = playercount.ToString();
            }
            GameTimePassed -= Time.deltaTime;
        }
        else
        {
            endtext.alpha = 255;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TMPro.TMP_Text playercounter;

    [SerializeField]
    int playercount;
    void Start()
    {
        playercount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playercount++;
            playercounter.text = playercount.ToString();
        }
    }
}

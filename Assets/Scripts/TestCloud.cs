using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCloud : MonoBehaviour
{
    [SerializeField]
    float panspeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.isPause())
        {
            transform.Translate(panspeed * Time.fixedDeltaTime, 0, 0);
        }
    }
}

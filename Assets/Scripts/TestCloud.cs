using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCloud : MonoBehaviour
{
    [SerializeField]
    float panspeed = 1;

    [System.NonSerialized]
    public GameObject[] killzone;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kill on collide w/ killzone
        for (int i = 0; i < killzone.Length; i++)
        {
            if (collision.gameObject == killzone[i])
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCloud : MonoBehaviour
{
    [SerializeField]
    public float panspeed = 6;

    [System.NonSerialized]
    public GameObject[] killzone;

    [System.NonSerialized]
    public Effectmanager manager;

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
                manager.effectArray.Remove(this.gameObject);
                Destroy(gameObject);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour
{
    Sprite obj;

    [SerializeField]
    bool counted;

    [SerializeField]
    float movespeed;

    [System.NonSerialized]
    public GameObject[] killzone;

    //[System.NonSerialized]
    public GameObject countzone;

    [System.NonSerialized]
    public TestManageMob manager;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Sprite>();
        counted = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movespeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kill on collide w killzone
        for(int i = 0; i < killzone.Length; i++)
        {
            if (collision.gameObject == killzone[i])
                Destroy(gameObject);
        }

        //count
        if (collision.gameObject == countzone)
        {
            manager.AddCount();
            counted = true;
        }
    }
}

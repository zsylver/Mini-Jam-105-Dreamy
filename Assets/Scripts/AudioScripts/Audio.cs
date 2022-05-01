using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void randomPitch(AudioSource audio)
    {
        audio.pitch = Random.Range(0.9f, 1.1f);
    }
}

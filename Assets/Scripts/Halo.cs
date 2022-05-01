using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    // Start is called before the first frame update
    float ogLocalY;
    Transform halo;
    float timer;
    float targetYPos;

    float duration = 0.5f;
    void Start()
    {
        timer = duration;
        halo = gameObject.transform;
        ogLocalY = halo.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            targetYPos = Random.Range(ogLocalY - 0.2f, ogLocalY + 0.2f);
            timer = duration;
        }
        halo.localPosition = Vector3.Lerp(halo.localPosition, 
            new Vector3(halo.localPosition.x, targetYPos, halo.localPosition.z), duration - timer);

    }
}

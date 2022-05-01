using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    // Start is called before the first frame update
    float ogLocalY;
    void Start()
    {
        ogLocalY = this.gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale.Set(this.gameObject.transform.localPosition.x,
            Random.Range(ogLocalY - 10f, ogLocalY + 10f), this.gameObject.transform.localPosition.z);
    }
}

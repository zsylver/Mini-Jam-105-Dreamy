using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    Animator animate;

    void Start()
    {
        animate = GetComponent<Animator>();
    }

    public void setBoolTru()
    {
        animate.SetBool("isHover", true);
    }
    public void setBoolFalse()
    {
        animate.SetBool("isHover", false);
    }
}

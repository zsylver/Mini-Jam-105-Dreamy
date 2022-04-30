using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonObject : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();

    bool triggered = false;
    // Use this for initialization

    [SerializeField]
    Sprite newSprite;

    Sprite ogSprite;
    void Start()
    {
        definedButton = this.gameObject;
        ogSprite = this.GetComponentInParent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (GameManager.Instance.isPause())
        {
            Debug.Log("BUTTON hover");

            if (Input.GetMouseButtonDown(0) && !triggered)
            {
                Debug.Log("BUTTON PRESSED");
                triggered = true;
                this.GetComponentInParent<SpriteRenderer>().sprite = newSprite;
                OnClick.Invoke();
            }
            else if (Input.GetMouseButtonUp(0) && triggered)
            {
                triggered = false;
                this.GetComponentInParent<SpriteRenderer>().sprite = ogSprite;
            }
        }


        
    }
    void Update()
    {
    }
}

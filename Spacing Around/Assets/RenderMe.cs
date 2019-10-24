using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMe : MonoBehaviour
{
    public Collider2D myCol;
    public Rigidbody2D myRig;
    public SpriteRenderer myRend;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
        myRig = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
    }

    void OnBecameInvisible()
    {
        //print("Im Gone..");
        //myRig.sleepMode = RigidbodySleepMode2D.StartAsleep;
        SwitchState(false);
    }

    void OnBecameVisible()
    {
        if (myCol == null)
        {
            myCol = GetComponent<Collider2D>();
        }
        //print("Im Back.!");
        //myRig.sleepMode = RigidbodySleepMode2D.NeverSleep;
        SwitchState(true);
    }

    void SwitchState(bool isOn)
    {
        if (isOn)
        {
            myCol.enabled = true;
            myRend.enabled = true;
        }
        else
        {
            myCol.enabled = false;
            myRend.enabled = false;
        }
    }
}
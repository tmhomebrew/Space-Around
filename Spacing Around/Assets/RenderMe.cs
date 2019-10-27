using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMe : MonoBehaviour
{
    public Rigidbody2D myRig;
    public SpriteRenderer myRend;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
        myRig = GetComponent<Rigidbody2D>();
    }

    void Visibility()
    {
        //myRig.sleepMode = RigidbodySleepMode2D.StartAsleep;
        SwitchState();
    }

    void SwitchState()
    {
        myRend.enabled = !myRend.enabled;
        print("Im .." + myRend.enabled);
    }
}
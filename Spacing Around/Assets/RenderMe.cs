﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMe : MonoBehaviour
{
    public Collider2D myParentCol;
    public SpriteRenderer myParentRendere;
    private Collider2D myCol;

    // Start is called before the first frame update
    void Start()
    {
        myParentRendere = GetComponentInParent<SpriteRenderer>();
        myCol = GetComponent<Collider2D>();
    }

    void SwitchState(bool isVisible)
    {
        if (myParentCol == null)
        {
            foreach (Collider2D col in GetComponentsInParent<Collider2D>())
            {
                if (col != myCol)
                {
                    myParentCol = col;
                    print(col);
                }
            }
        }

        if (isVisible)
        {
            myParentRendere.enabled = true;
            //myParentCol.enabled = true;
            print("Im visible.!");
        }
        else
        {
            myParentRendere.enabled = false;
            //myParentCol.enabled = false;
            print("Im invisible.!");
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("EnterStuff");
        if (col.transform.root.tag == "Player")
        {
            SwitchState(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        print("ExitStuff");
        if (col.transform.root.tag == "Player")
        {
            SwitchState(false);
        }
    }
}
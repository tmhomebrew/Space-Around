using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatToSee : MonoBehaviour
{
    private Collider2D myCol;

    [SerializeField]
    private float myColSize;
    public float MyColSize
    {
        get => myColSize; 
        set
        {
            myColSize = value;
            myCol.transform.localScale = new Vector3(myColSize / 10, myColSize / 10);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Astroid")
        {
            try
            {
                col.transform.GetComponentInChildren<RenderMe>().SwitchState(true);
            }
            catch (System.Exception)
            {
                print("Could not use the SwitchState(true)");
                throw;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Astroid") 
        { 
            try
            {
                col.transform.GetComponentInChildren<RenderMe>().SwitchState(false);
            }
            catch (System.Exception)
            {
                print("Could not use the SwitchState(false)");
                throw;
            }
        }
    }
}

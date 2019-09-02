using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotater : MonoBehaviour
{
    private GameObject astroidPointer;
    private Vector3 astroidDir;

    public Vector3 AstroidDir
    {
        get
        {
            return astroidDir = astroidPointer.GetComponent<Transform>().position - transform.position;
        }

        set
        {
            astroidDir = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        astroidPointer.transform.RotateAround(transform.position, Vector3.forward, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaValue : MonoBehaviour
{
    public int alphaValue;
    public GameObject child;

    public Material[] mats = new Material[3];

    public void AddChildToGO(GameObject go)
    {
        go.transform.parent = transform;
        child = go;
        ChangeAlphaValue();
    }

    void ChangeAlphaValue()
    {
        if (alphaValue == 0)
        {
            child.GetComponent<MeshRenderer>().material = mats[2];
        }
        if (alphaValue == 1)
        {
            child.GetComponent<MeshRenderer>().material = mats[1];
        }
        if (alphaValue == 2)
        {
            child.GetComponent<MeshRenderer>().material = mats[0];
        }
    }
}

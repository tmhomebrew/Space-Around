using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRotater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotater());
    }

    IEnumerator Rotater()
    {
        while (true)
        {
            transform.RotateAround(transform.position, Vector3.forward, 0.05f);
            yield return null;
        }
    }
}

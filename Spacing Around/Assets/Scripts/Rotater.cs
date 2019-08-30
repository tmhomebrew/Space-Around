using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    bool isRunning; //For Inspector Purpose
    public float RotationSpeed = 200f;
    Quaternion startRot;

    private void Start()
    {
        isRunning = false;
        startRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        ClickedOnObject();
    }

    public void ClickedOnObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(RotateTowardsStartingPoint());
            isRunning = false;
        }
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(
                (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime),
                (Input.GetAxis("Mouse X") * -RotationSpeed * Time.deltaTime), 
                0, 
                Space.World
            );
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (transform.localRotation != startRot)
            {
                StartCoroutine(RotateTowardsStartingPoint());
            }
        }
    }

    IEnumerator RotateTowardsStartingPoint()
    {
        isRunning = true;
        while (transform.localRotation != startRot)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startRot, RotationSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            if (transform.localRotation == startRot)
            {
                break;
            }
        }
        isRunning = false;
    }
}

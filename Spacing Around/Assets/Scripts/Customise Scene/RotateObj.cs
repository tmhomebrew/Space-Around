using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    bool isRotating, isZooming, canRotate, canZoom; //For Inspector Purpose
    public float RotationSpeed = 200f;
    Quaternion startRot;
    Vector3 startScale;

    private void Start()
    {
        isRotating = false;
        isZooming = false;
        canRotate = false;
        canZoom = false;

        startRot = transform.localRotation;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ClickedOnObject();
    }

    public void ClickedOnObject()
    {
        if (canRotate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(RotateTowardsStartingPoint());
                isRotating = false;
            }
            if (Input.GetMouseButton(0))
            {
                transform.Rotate(
                    (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime),
                    (Input.GetAxis("Mouse X") * -RotationSpeed * Time.deltaTime), 
                    0, 
                    Space.Self
                );
            }
        }
        if (canZoom)
        {
            if (Input.mouseScrollDelta.y != 0 && !isZooming)
            {
                print("Mouse Scroll Input - " + Input.mouseScrollDelta.y * 0.1f);
                //Zoom in
                if (Input.mouseScrollDelta.y > 0)
                {
                    StartCoroutine(ZoomInOnObject(0.1f, Vector3.one * 1.1f));
                }
                //Zoom out
                if (Input.mouseScrollDelta.y < 0)
                {
                    StartCoroutine(ZoomInOnObject(0.1f, Vector3.one * -1.1f));
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (transform.localRotation != startRot)
            {
                StartCoroutine(RotateTowardsStartingPoint());
            }
            if (transform.localScale != startScale)
            {
                StartCoroutine(ZoomInOnObject(0.5f, startScale));
            }
        }
    }

    IEnumerator RotateTowardsStartingPoint()
    {
        isRotating = true;
        while (transform.localRotation != startRot && isRotating == true)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startRot, RotationSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            if (transform.localRotation == startRot)
            {
                break;
            }
        }
        isRotating = false;
    }

    Vector3 originalScale, targetScale;

    IEnumerator ZoomInOnObject(float time, Vector3 zoomScale)
    {
        isZooming = true;
        if (zoomScale != startScale)
        {
            originalScale = transform.localScale;
            targetScale = transform.localScale + zoomScale;
        }
        else
        {
            originalScale = transform.localScale;
            targetScale = zoomScale;
        }
        float originalTime = time;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;

            if (targetScale.y > 5 || targetScale.y < 1)
            {
                if (targetScale.y > 5)
                {
                    transform.localScale = Vector3.one * 5;
                }
                if (targetScale.y < 1)
                {
                    transform.localScale = Vector3.one;
                }
                break;
            }
            else
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, time / originalTime);
            }
            yield return null;
        }
        isZooming = false;
    }
}

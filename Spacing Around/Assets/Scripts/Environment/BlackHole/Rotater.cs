using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float rotationSpeed;
    [SerializeField]
    private bool isGameRunning;

    public bool IsGameRunning
    {
        get => isGameRunning; set
        {
            isGameRunning = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 1f;
        RotationController(IsGameRunning);
    }

    public void RotationController(bool run)
    {
        if (run)
        {
            IsGameRunning = true;
            StartCoroutine(RotateWorld());
        }
        else
        {
            IsGameRunning = false;
            StopCoroutine(RotateWorld());
        }
    }

    IEnumerator RotateWorld()
    {
        while (IsGameRunning)
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

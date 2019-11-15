using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    private ShipStats shipStats;
    private WhatToSee myWTS;

    private Vector3 offset;
    private Camera myCam;
    [Range(10, 15)]
    private float startDistanceFromPlayer;
    private float maxDistanceFromPlayer;
    bool sizeReached, zoomingIsRunning;
    private float delay = 0.1f;

    public bool ZoomingIsRunning
    {
        get => zoomingIsRunning;
        set
        {
            StopAllCoroutines();
            zoomingIsRunning = shipStats.IsMoving;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        shipStats = player.GetComponent<ShipStats>();
        myWTS = player.GetComponentInChildren<WhatToSee>();

        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;

        sizeReached = true;
        ZoomingIsRunning = false;
        startDistanceFromPlayer = 10;
        maxDistanceFromPlayer = 15;
        myCam.orthographicSize = startDistanceFromPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        if (shipStats.ShipSpeedCur > 0 && shipStats.IsMoving)
        {
            ChangeDistance(shipStats.IsMoving);
        }
        if (!shipStats.IsMoving)
        {
            ChangeDistance(shipStats.IsMoving);
        }
    }

    void ChangeDistance(bool isMoving)
    {
        StartCoroutine(DistChanger(isMoving));
    }

    IEnumerator DistChanger(bool moving)
    {
        if (myCam.orthographicSize > startDistanceFromPlayer || myCam.orthographicSize < maxDistanceFromPlayer)
        {
            sizeReached = false;
        }
        if (moving)
        {
        //print("Enumarator running");
            while (myCam.orthographicSize < maxDistanceFromPlayer && sizeReached == false)
            {
                yield return new WaitForSeconds(delay);
                myCam.orthographicSize += Time.deltaTime;
                myWTS.MyColSize = myCam.orthographicSize;
                if (myCam.orthographicSize > maxDistanceFromPlayer)
                {
                    myCam.orthographicSize = maxDistanceFromPlayer;
                    myWTS.MyColSize = maxDistanceFromPlayer;
                    sizeReached = true;
                    break;
                }
            }
        }
        else
        {
            while (myCam.orthographicSize > startDistanceFromPlayer && sizeReached == false)
            {
                yield return new WaitForSeconds(delay);
                myCam.orthographicSize -= Time.deltaTime;
                myWTS.MyColSize = myCam.orthographicSize;
                if (myCam.orthographicSize < startDistanceFromPlayer)
                {
                    myCam.orthographicSize = startDistanceFromPlayer;
                    myWTS.MyColSize = startDistanceFromPlayer;
                    sizeReached = true;
                    break;
                }
            }
        }
    }
}

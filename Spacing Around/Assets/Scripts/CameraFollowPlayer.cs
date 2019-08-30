using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    private ShipStats shipStats;
    private Vector3 offset;
    private Camera myCam;
    [Range(10, 15)]
    private float startDistanceFromPlayer;
    private float maxDistanceFromPlayer;
    private bool sizeReached;
    private float delay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        shipStats = player.GetComponent<ShipStats>();

        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;

        sizeReached = true;
        startDistanceFromPlayer = 10;
        maxDistanceFromPlayer = 15;
        myCam.orthographicSize = startDistanceFromPlayer;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        if (shipStats.ShipSpeedCur > 0 && shipStats.IsMoving)
            ChangeDistance(shipStats.IsMoving);
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
        sizeReached = false;
        if (moving)
        {
            while (myCam.orthographicSize < maxDistanceFromPlayer && sizeReached == false)
            {
                yield return new WaitForSeconds(delay);
                myCam.orthographicSize += Time.deltaTime;
                if (myCam.orthographicSize > maxDistanceFromPlayer)
                {
                    myCam.orthographicSize = maxDistanceFromPlayer;
                    sizeReached = true;
                }
            }
        }
        else
        {
            while (myCam.orthographicSize > startDistanceFromPlayer && sizeReached == false)
            {
                yield return new WaitForSeconds(delay);
                myCam.orthographicSize -= Time.deltaTime;
                if (myCam.orthographicSize < startDistanceFromPlayer)
                {
                    myCam.orthographicSize = startDistanceFromPlayer;
                    sizeReached = true;
                }
            }
        }
    }

}

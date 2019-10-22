using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject laserShot;
    GameObject laserShotOwner;

    public GameObject spawnPoint;
    public Transform laserHolder;

    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }

    public void ShotLaser()
    {
        //Instantiate(laserShot, spawnPoint.transform.position, spawnPoint.transform.rotation, laserHolder);
        Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);
    }
}

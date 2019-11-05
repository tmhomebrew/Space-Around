using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public enum LaserType
    {
        Green,
        LightBlue,
        Blue,
        Yellow,
        Red,
        Purple
    }

    public GameObject laserShot;
    [SerializeField]
    public Sprite[] laserBeamSprite = new Sprite[5];
    [SerializeField]
    LaserType gunLaserType;

    public GameObject spawnPoint;
    public Transform laserHolder;

    GameObject laserShotOwner;
    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }
    public LaserType GunLaserType { get => gunLaserType; set => gunLaserType = value; }


    public void Start()
    {
        GunLaserType = LaserType.Green;
    }

    public void ShotLaser()
    {
        //Instantiate(laserShot, spawnPoint.transform.position, spawnPoint.transform.rotation, laserHolder);
        //GameObject currentShot = laserShot;
        //currentShot.GetComponent<LaserShot>().SetupLaserStats((int)GunLaserType);
        Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);
    }
}
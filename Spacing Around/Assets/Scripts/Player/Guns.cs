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
    private Sprite[] laserBeamSprite = new Sprite[5];
    [SerializeField]
    LaserType gunLaserType;

    public GameObject spawnPoint;
    public Transform laserHolder;

    //Object pool
    [SerializeField]
    private PrefabPooling laserPool;
    GameObject newLaserObj;

    GameObject laserShotOwner;
    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }
    public LaserType GunLaserType { get => gunLaserType; set => gunLaserType = value; }
    public Sprite[] LaserBeamSprite { get => laserBeamSprite; set => laserBeamSprite = value; }

    public void Awake()
    {
        if (laserPool == null)
        {
            laserPool = transform.root.GetComponentInChildren<PrefabPooling>();
        }
    }

    public void Start()
    {
        if (laserHolder == null)
        {
            try
            {
                foreach (Transform go in transform.root.GetComponentInChildren<Transform>())
                {
                    if (go.gameObject.name.Contains("LaserShotHolder"))
                    {
                        laserHolder = go;
                        break;
                    }
                }
            }
            catch (System.Exception)
            {
                print("Could not find 'LaserShotHolder'..");
                throw;
            }
        }
        GunLaserType = LaserType.Purple;
    }

    public void ShotLaser()
    {
        if (laserPool.GetAvailableObject() != null)
        {
            newLaserObj = laserPool.GetAvailableObject();
            newLaserObj.GetComponent<LaserShot>().MyGun = GetComponent<Guns>();
            newLaserObj.GetComponent<LaserShot>().SetupLaserStats((int)GunLaserType);
            newLaserObj.transform.SetParent(laserHolder);
            newLaserObj.transform.position = spawnPoint.transform.position;
            newLaserObj.transform.rotation = transform.rotation;
            newLaserObj.SetActive(true);
        }

        //Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);
        //laserShot.GetComponent<LaserShot>().MyGun = GetComponent<Guns>();
        //laserShot.GetComponent<LaserShot>().SetupLaserStats((int)GunLaserType);
    }
}
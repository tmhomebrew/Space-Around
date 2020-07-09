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

    private GameObject laserShot, lasershotNew;
    GameObject laserShotOwner;
    [SerializeField]
    private Sprite[] laserBeamSprite = new Sprite[5];
    [SerializeField]
    LaserType gunLaserType;

    public GameObject spawnPoint;
    public Transform laserHolder;

    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }
    public LaserType GunLaserType { get => gunLaserType; set => gunLaserType = value; }
    public Sprite[] LaserBeamSprite { get => laserBeamSprite; set => laserBeamSprite = value; }
    public GameObject LasershotNew { get => lasershotNew; set => lasershotNew = value; }

    public void Awake()
    {
        //GunLaserType = LaserType.Purple;
        LasershotNew = new GameObject();
    }

    public void Start()
    {
        if (laserHolder == null)
        {
            try
            {
                foreach (Transform go in transform.root.GetComponentInChildren<Transform>())
                {
                    if (go.gameObject.name.ToLower().Contains("lasershotholder"))
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
    }

    public void ShotLaser()
    {
        Instantiate(LasershotNew, spawnPoint.transform.position, transform.rotation, laserHolder);

        #region Old
        //Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);

        //laserShot.GetComponent<LaserShot>().MyGun = GetComponent<Guns>();
        //laserShot.GetComponent<LaserShot>().SetupLaserStats((int)GunLaserType);
        #endregion
    }
}
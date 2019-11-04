using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    [SerializeField]
    Sprite mySprite;
    Rigidbody2D myRB;
    SpriteRenderer mySprRend;
    [SerializeField]
    Guns myGun;
    
    [SerializeField]
    private GameObject laserOwner;
    [SerializeField]
    private float speed;
    private int damage;



    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }

    public void SetupLaserStats(int myGLT)
    {
        switch (myGLT)
        {
            //case Guns.LaserType.Green:
            case 0:
                speed = 20f;
                damage = 1;
                mySprite = myGun.laserBeamSprite[0];
                break;
            //case Guns.LaserType.LightBlue:
            case 1:
                speed = 20f;
                damage = 2;
                mySprite = myGun.laserBeamSprite[1];
                break;
            //case Guns.LaserType.Blue:
            case 2:
                speed = 25f;
                damage = 4;
                mySprite = myGun.laserBeamSprite[2];
                break;
            //case Guns.LaserType.Yellow:
            case 3:
                speed = 25f;
                damage = 6;
                mySprite = myGun.laserBeamSprite[3];
                break;
            //case Guns.LaserType.Red:
            case 4:
                speed = 40f;
                damage = 12;
                mySprite = myGun.laserBeamSprite[4];
                break;
           // case Guns.LaserType.Purple:
            case 5:
                speed = 50f;
                damage = 20;
                mySprite = myGun.laserBeamSprite[5];
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        if (transform.root.tag == "Player")
        {
            LaserOwner = transform.root.transform.GetComponentInChildren<ShipStats>().gameObject;
        }
        else if (transform.root.tag == "Enemy")
        {
            LaserOwner = transform.root.transform.GetComponentInChildren<EnemyShipStats>().gameObject;
        }
        //myWL = LaserOwner.GetComponentInChildren<WeaponLaser>(); //Need this??

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());

        if (myGun == null)
        {
            myGun = GetComponent<Guns>();
        }
        myRB = GetComponent<Rigidbody2D>();
        mySprRend = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeAlive());
        //SetupLaserStats();
        //speed = 20f;
        myRB.AddForce(SpeedOfLaser(speed), ForceMode2D.Impulse);
    }

    private Vector3 SpeedOfLaser(float speed)
    {
        return transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (col.gameObject != LaserOwner)
        {
            //print("Hit player: " + col.gameObject.name);
            StopAllCoroutines();
            StartCoroutine(DestroySequence());
        }
    }

    IEnumerator DestroySequence()
    {
        //Animation for LaserHit

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    { 
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

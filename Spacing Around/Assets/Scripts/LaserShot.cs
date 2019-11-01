using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    WeaponLaser myWL;

    public enum LaserType
    {
        Green,
        LightBlue,
        Blue,
        Yellow,
        Red,
        Purple
    }

    [SerializeField]
    public Sprite[] laserBeamSprite = new Sprite[5];
    [SerializeField]
    Sprite mySprite;
    LaserType myLT;
    //Mangler reference til WeaponLaser........... Construct ? <-----

    [SerializeField]
    private GameObject laserOwner;
    [SerializeField]
    private float speed;
    private int damage;

    Rigidbody2D myRB;

    SpriteRenderer mySprRend;


    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }

    public LaserShot(LaserType myLT)
    {
        switch (myWL.LaserRarity)
        {
            case BaseItem.Rarity.Common:
                myLT = LaserType.Blue;        
                break;
            default:
                break;
        }
        SetupLaserStats(myLT); //<--- In progress..

    }

    void SetupLaserStats(LaserType typeOfLaser)
    {
        switch (typeOfLaser)
        {
            case LaserType.Green:
                speed = 20f;
                damage = 1;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.LightBlue:
                speed = 20f;
                damage = 2;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.Blue:
                speed = 25f;
                damage = 4;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.Yellow:
                speed = 25f;
                damage = 6;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.Red:
                speed = 40f;
                damage = 12;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.Purple:
                speed = 50f;
                damage = 20;
                mySprite = laserBeamSprite[1];
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
        myWL = LaserOwner.GetComponentInChildren<WeaponLaser>(); //Need this??

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
        
        myRB = GetComponent<Rigidbody2D>();
        mySprRend = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeAlive());
        
        speed = 20f;
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
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

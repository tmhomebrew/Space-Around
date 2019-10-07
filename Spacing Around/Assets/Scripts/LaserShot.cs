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
    Sprite mySprite;
    LaserType myLT;
    //Mangler reference til WeaponLaser........... Construct ? <-----

    [SerializeField]
    private GameObject laserOwner;
    [SerializeField]
    private float speed;

    Rigidbody2D myRB;

    SpriteRenderer mySprRend;


    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }

    public LaserShot()
    {
        //switch (myWL.LaserRarity)
        //{
        //    case BaseItem.Rarity.Common:
        //        myLT = LaserType.Blue;
        //        mySprite = laserBeamSprite[1];
        //        break;
        //    default:
        //        break;
        //}
        
    }

    void Awake()
    {
        LaserOwner = transform.root.transform.GetComponentInChildren<ShipStats>().gameObject;
        myWL = LaserOwner.GetComponentInChildren<WeaponLaser>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
        myRB = GetComponent<Rigidbody2D>();

        mySprRend = GetComponent<SpriteRenderer>();
        //SetStats(LaserType.Green);
        //mySprRend.sprite = mySprite;  <---------------------------------------------
        
        
        //Destroy(GetComponent<PolygonCollider2D>()); //Refreshing the Collider
        //gameObject.AddComponent<PolygonCollider2D>(); //Refreshing the Collider

    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
        StartCoroutine(TimeAlive());

        myRB.AddForce(SpeedOfLaser(speed), ForceMode2D.Impulse);
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

    private Vector3 SpeedOfLaser(float speed)
    {
        return transform.up * speed;
    }

    IEnumerator DestroySequence()
    {
        //Animation for LaserHit

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    { 
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

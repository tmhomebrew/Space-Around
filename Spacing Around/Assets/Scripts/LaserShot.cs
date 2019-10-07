using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public enum LaserType
    {
        Green,
        Blue,
        Yellow,
        Red,
        Purple
    }

    //Mangler reference til WeaponLaser........... Construct ? <-----

    [SerializeField]
    private GameObject laserOwner;
    [SerializeField]
    private float speed;

    Rigidbody2D myRB;

    Vector3 forwardSpeed;

    SpriteRenderer mySprRend;

    [SerializeField]
    public Sprite[] laserBeamSprite = new Sprite[5];
    Sprite mySprite;

    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }

    void Awake()
    {
        LaserOwner = transform.root.transform.GetComponentInChildren<ShipStats>().gameObject;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
        myRB = GetComponent<Rigidbody2D>();

        mySprRend = GetComponent<SpriteRenderer>();
        //Destroy(GetComponent<PolygonCollider2D>()); //Refreshing the Collider
        //gameObject.AddComponent<PolygonCollider2D>(); //Refreshing the Collider

        //SetStats(LaserType.Green);
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

    //public void SetStats(LaserType LT)
    //{
    //    speed = 10;
    //    switch (LT)
    //    {
    //        case LaserType.Green:
    //            damage = 2;
    //            mySprite = laserBeamSprite[0];
    //            break;
    //        case LaserType.Blue:
    //            damage = 4;
    //            mySprite = laserBeamSprite[1];
    //            break;
    //        case LaserType.Yellow:
    //            mySprite = laserBeamSprite[2];
    //            break;
    //        case LaserType.Red:
    //            mySprite = laserBeamSprite[3];
    //            break;
    //        case LaserType.Purple:
    //            damage = 100;
    //            mySprite = laserBeamSprite[4];
    //            break;
    //        default:
    //            break;
    //    }
    //    mySprRend.sprite = mySprite;
    //}
}

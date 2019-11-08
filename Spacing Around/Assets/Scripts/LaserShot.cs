using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Guns myGun;
    
    [SerializeField]
    private GameObject laserOwner;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

    #endregion
    #region Properties
    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }
    public Guns MyGun { get => myGun; set => myGun = value; }
    public int Damage { get => damage; set => damage = value; }

    #endregion

    public void SetupLaserStats(int myGLT)
    {
        switch (myGLT)
        {
            //case Guns.LaserType.Green:
            case 0:
                speed = 20f;
                Damage = 1;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[0];
                break;
            //case Guns.LaserType.LightBlue:
            case 1:
                speed = 20f;
                Damage = 2;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[1];
                break;
            //case Guns.LaserType.Blue:
            case 2:
                speed = 25f;
                Damage = 4;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[2];
                break;
            //case Guns.LaserType.Yellow:
            case 3:
                speed = 25f;
                Damage = 6;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[3];
                break;
            //case Guns.LaserType.Red:
            case 4:
                speed = 40f;
                Damage = 12;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[4];
                break;
           // case Guns.LaserType.Purple:
            case 5:
                speed = 50f;
                Damage = 20;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[5];
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
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeAlive());
        GetComponent<Rigidbody2D>().AddForce(SpeedOfLaser(speed), ForceMode2D.Impulse);
    }

    private Vector3 SpeedOfLaser(float varSpeed)
    {
        return transform.up * varSpeed;
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

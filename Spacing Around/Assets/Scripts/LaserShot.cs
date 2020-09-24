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
    private float flySpeed, timeAlive;
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
                flySpeed = 20f;
                Damage = 1;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[0];
                break;
            //case Guns.LaserType.LightBlue:
            case 1:
                flySpeed = 20f;
                Damage = 2;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[1];
                break;
            //case Guns.LaserType.Blue:
            case 2:
                flySpeed = 25f;
                Damage = 4;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[2];
                break;
            //case Guns.LaserType.Yellow:
            case 3:
                flySpeed = 25f;
                Damage = 6;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[3];
                break;
            //case Guns.LaserType.Red:
            case 4:
                flySpeed = 40f;
                Damage = 12;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[4];
                break;
           // case Guns.LaserType.Purple:
            case 5:
                flySpeed = 50f;
                Damage = 20;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[5];
                break;
            default:
                break;
        }
    }

    private void SetOwnerOfShot()
    {
        if (transform.root.CompareTag("Player"))
        {
            LaserOwner = transform.root.transform.GetComponentInChildren<ShipStats>().gameObject;
        }
        else if (transform.root.CompareTag("Enemy"))
        {
            LaserOwner = transform.root.transform.GetComponentInChildren<EnemyShipStats>().gameObject;
        }

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
    }

    private void OnEnable()
    {
        SetOwnerOfShot();
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        timeAlive = 1.5f;
        StartCoroutine(TimeAlive(timeAlive));
        GetComponent<Rigidbody2D>().AddForce(SpeedOfLaser(flySpeed), ForceMode2D.Impulse);
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
            StopAllCoroutines();
            StartCoroutine(DestroySequence());
        }
    }

    IEnumerator DestroySequence()
    {
        //Animation for LaserHit
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    IEnumerator TimeAlive(float time)
    { 
        //Animation for LaserRunOutOfTime?
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

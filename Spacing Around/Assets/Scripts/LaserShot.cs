using System.Collections;
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
                Damage = 3;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[0];
                break;
            //case Guns.LaserType.LightBlue:
            case 1:
                flySpeed = 20f;
                Damage = 5;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[1];
                break;
            //case Guns.LaserType.Blue:
            case 2:
                flySpeed = 25f;
                Damage = 8;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[2];
                break;
            //case Guns.LaserType.Yellow:
            case 3:
                flySpeed = 25f;
                Damage = 10;
                GetComponent<SpriteRenderer>().sprite = MyGun.LaserBeamSprite[3];
                break;
            //case Guns.LaserType.Red:
            case 4:
                flySpeed = 40f;
                Damage = 15;
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

    /// <summary>
    /// Sets owner of LaserShot, each time a new GameObj is enabled.
    /// Shot ignores collision with its owner.
    /// </summary>
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

    /// <summary>
    /// Multiplies speed of Lasershot and transform.up.
    /// </summary>
    /// <param name="varSpeed">Speed of laserShot.</param>
    /// <returns>Vector3 dir and speed multiplied.</returns>
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
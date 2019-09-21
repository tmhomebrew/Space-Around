using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    [SerializeField]
    private GameObject laserOwner;
    public float speed;

    Rigidbody2D myRB;

    Vector3 forwardSpeed;

    public GameObject LaserOwner { get => laserOwner; set => laserOwner = value; }

    // Start is called before the first frame update
    void Start()
    {
        LaserOwner = transform.root.transform.GetComponentInChildren<ShipStats>().gameObject;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LaserOwner.GetComponentInChildren<Collider2D>());
        //print("LaserShots Owner: " + LaserOwner);

        StartCoroutine(TimeAlive());

        speed = 20f;
        myRB = GetComponent<Rigidbody2D>();
        forwardSpeed = (transform.up * speed);
        myRB.AddForce(forwardSpeed, ForceMode2D.Impulse);
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

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    { 
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

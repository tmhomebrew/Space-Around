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
        print("LaserShots Owner: " + LaserOwner);

        StartCoroutine(TimeAlive());

        speed = 20f;
        myRB = GetComponent<Rigidbody2D>();
        forwardSpeed = (transform.up * speed);
        myRB.AddForce(forwardSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == LaserOwner)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.collider);
            print("passed player");
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DestroySequence());
        }
    }

    IEnumerator DestroySequence()
    {
        //Animation for LaserHit

        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }

    IEnumerator TimeAlive()
    { 
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

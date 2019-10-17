using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D myRB;
    ShipStats myShip;
    Guns myGuns;
    WeaponLaser myWL;

    WrapScreenHandler myWrap;
    IEnumerator wrapChecker;

    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myShip = GetComponent<ShipStats>();
        //myWrap = GetComponent<WrapScreenHandler>();
        myGuns = GetComponentInChildren<Guns>();
        myGuns.LaserShotOwner = gameObject;
        myWL = GetComponentInChildren<WeaponLaser>();
        myWL.LaserShotOwner = gameObject;

        //wrapChecker = myWrap.CheckVisable(); //Inside Bounds on GameMap..

        curSpeed = myShip.ShipSpeedCur;
        accSpeed = myShip.ShipAcceleration;
        maxRotationSpeed = myShip.ShipTurnSpeed;
        maxSpeed = myShip.ShipSpeedMax;
    }

    private void Update()
    {
        if (myShip.IsAlive)
        {
            MoveController();
            DirectionController();
            Actions();
        }
    }

    private void Actions()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            myGuns.ShotLaser();
            myWL.Shoot();
        }
    }

    private void MoveController()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            myShip.IsMoving = true;
            forwardSpeed = (transform.up * (curSpeed + accSpeed) * Time.deltaTime); //<---

            if (curSpeed < maxSpeed)
            {
                myRB.AddForce(forwardSpeed, ForceMode2D.Force);
                //print("adding Speed..");
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            myShip.IsMoving = false;
        }
        //StartCoroutine(myWrap.CheckVisable()); //Checks position on level..
        curSpeed = myRB.velocity.magnitude;
        myShip.ShipSpeedCur = curSpeed;
        
        //print("CurrentSpeed: " + curSpeed);
    }

    private void DirectionController()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1) * maxRotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1) * -maxRotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
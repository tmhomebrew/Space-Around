using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Fields
    Rigidbody2D myRB;
    ShipStats myShip;
    Guns myGuns;

    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed;

    private bool newWeaponSelection = false; //Used for ChooseWeapon()

    #endregion

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myShip = GetComponent<ShipStats>();
        myGuns = GetComponentInChildren<Guns>();
        myGuns.LaserShotOwner = gameObject;

        curSpeed = myShip.ShipSpeedCur;
        accSpeed = myShip.ShipAcceleration;
        maxRotationSpeed = myShip.ShipTurnSpeed;
        maxSpeed = myShip.ShipSpeedMax;
    }

    private void Update()
    {
        if (myShip.IsAlive)
        {
            ChooseWeapon();
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
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            myShip.IsMoving = false;
        }
        curSpeed = myRB.velocity.magnitude;
        myShip.ShipSpeedCur = curSpeed;
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

    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myGuns.GunLaserType = Guns.LaserType.Green;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myGuns.GunLaserType = Guns.LaserType.LightBlue;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myGuns.GunLaserType = Guns.LaserType.Blue;
            newWeaponSelection = true;
        }
        if (newWeaponSelection)
        {
            myGuns.laserShot.GetComponent<LaserShot>().SetupLaserStats((int)myGuns.GunLaserType);
            newWeaponSelection = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Fields
    //References
    Rigidbody2D myRB;
    ShipStats myShip;

    //Gun system
    List<GameObject> myGuns;
    Guns.LaserType newLaserType;

    //Speed variables
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
        myGuns = new List<GameObject>();
        SetupGuns();

        curSpeed = myShip.ShipSpeedCur;
        accSpeed = myShip.ShipAcceleration;
        maxRotationSpeed = myShip.ShipTurnSpeed;
        maxSpeed = myShip.ShipSpeedMax;
    }

    private void SetupGuns()
    {
        foreach (Guns gu in GetComponentsInChildren<Guns>())
        {
            gu.gameObject.GetComponent<Guns>().LaserShotOwner = gameObject;
            myGuns.Add(gu.gameObject);
        }
    }

    private void Update()
    {
        if (myShip.IsAlive)
        {
            ChooseWeapon();
            CombinationChecker();
            MoveController();
            DirectionController();
            Actions();
        }
    }

    #region KeyCombs
    private KeyCombo barrolRollRight = new KeyCombo(new string[] { "left", "right" });
    private KeyCombo barrolRollLeft = new KeyCombo(new string[] { "right", "left" });

    #endregion

    private void CombinationChecker()
    {
        if (barrolRollRight.Check())
        {
            // do the barrol roll to the right
            Debug.Log("BarrolRollRight has been executed.!");
        }
        if (barrolRollLeft.Check())
        {
            // do the barrol roll to the left
            Debug.Log("BarrolRollLeft has been executed.!");
        }
    }

    private void Actions()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (GameObject gu in myGuns)
            {
                gu.GetComponent<Guns>().ShotLaser();
            }
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
            newLaserType = Guns.LaserType.Green;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            newLaserType = Guns.LaserType.LightBlue;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newLaserType = Guns.LaserType.Blue;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            newLaserType = Guns.LaserType.Yellow;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            newLaserType = Guns.LaserType.Red;
            newWeaponSelection = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            newLaserType = Guns.LaserType.Purple;
            newWeaponSelection = true;
        }
        if (newWeaponSelection)
        {
            //myGuns.laserShot.GetComponent<LaserShot>().SetupLaserStats((int)myGuns.GunLaserType);
            foreach (GameObject go in myGuns)
            {
                go.GetComponent<Guns>().GunLaserType = newLaserType;
                //print("Gun is.: " + go.GetComponent<Guns>().GunLaserType);
            }
            newWeaponSelection = false;
        }
    }
}
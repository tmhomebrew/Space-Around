using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Fields
    Rigidbody2D myRB;
    ShipStats myShip;
<<<<<<< HEAD
    Animation myAni;
    Animator myAniTor;
=======
    Guns myGuns;
>>>>>>> parent of 73fd045... Small fixes + KeyCombos!

    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed;

<<<<<<< HEAD
    //Animation
    //private bool aniIsPlaying;

    private bool newWeaponSelection = true; //Used for ChooseWeapon()
=======
    private bool newWeaponSelection = false; //Used for ChooseWeapon()
>>>>>>> parent of 73fd045... Small fixes + KeyCombos!

    #endregion

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myShip = GetComponent<ShipStats>();
<<<<<<< HEAD
        myGuns = new List<GameObject>();
        //myAni = GetComponentInParent<Animation>();
        //myAniTor = GetComponentInParent<Animator>();
        SetupGuns();
=======
        myGuns = GetComponentInChildren<Guns>();
        myGuns.LaserShotOwner = gameObject;
>>>>>>> parent of 73fd045... Small fixes + KeyCombos!

        curSpeed = myShip.ShipSpeedCur;
        accSpeed = myShip.ShipAcceleration;
        maxRotationSpeed = myShip.ShipTurnSpeed;
        maxSpeed = myShip.ShipSpeedMax;

        //aniIsPlaying = false;
    }

    private void Update()
    {
        if (myShip.IsAlive)
        {
            ChooseWeapon();
<<<<<<< HEAD
            //if (aniIsPlaying == false)
            //{
                CombinationChecker();
            //}
=======
>>>>>>> parent of 73fd045... Small fixes + KeyCombos!
            MoveController();
            //if (aniIsPlaying == false)
            //{
                DirectionController();
            //}
            Actions();
        }
    }

<<<<<<< HEAD
    #region KeyCombs
    private KeyCombo barrolRollRight = new KeyCombo(new string[] { "left", "right" }, 0.2f);
    private KeyCombo barrolRollLeft = new KeyCombo(new string[] { "right", "left" }, 0.2f);

    #endregion

    private void CombinationChecker()
    {
        if (barrolRollRight.Check())
        {
            //StartCoroutine( RunAnimation(false) );
            // do the barrol roll to the right
            Debug.Log("BarrolRollRight has been executed.!");
        }
        if (barrolRollLeft.Check())
        {
            //StartCoroutine( RunAnimation(true) );
            // do the barrol roll to the left
            Debug.Log("BarrolRollLeft has been executed.!");
        }
    }

=======
>>>>>>> parent of 73fd045... Small fixes + KeyCombos!
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
    
    //private IEnumerator RunAnimation(bool leftSide)
    //{
    //    aniIsPlaying = true;
    //    myAniTor.SetBool("DirIsLeft", leftSide);
    //    myAniTor.SetTrigger("RunAnimation");

    //    yield return new WaitForSeconds(1);

    //    aniIsPlaying = false;
    //}
}
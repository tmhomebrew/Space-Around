using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Fields
    //References
    Rigidbody2D myRB;
    ShipStats myShip;
    Animation myAni;

    //Gun system
    List<GameObject> myGuns;
    Guns.LaserType newLaserType;

    //Speed variables
    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed;

    private bool newWeaponSelection = true; //Used for ChooseWeapon()
    public bool canMove = true;

    #endregion

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myShip = GetComponent<ShipStats>();
        myGuns = new List<GameObject>();
        myAni = GetComponentInChildren<Animation>();
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
            if (canMove)
            {
                MoveController();
                DirectionController();
            }
            Actions();
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
            }
            newWeaponSelection = false;
        }
    }

    #region KeyCombs
    private KeyCombo barrolRollRight = new KeyCombo(new string[] { "left", "right" });
    private KeyCombo barrolRollLeft = new KeyCombo(new string[] { "right", "left" });
    private float barralRollLength = 3;
    private bool canUseCombo = true;

    #endregion
    private void CombinationChecker()
    {
        if (canUseCombo)
        {
            if (barrolRollRight.Check())
            {
                StartCoroutine(CombinationDelay());
                //StartCoroutine(MoveTo(transform, transform.TransformDirection(transform.position + (Vector3.left * barralRollLength)), myAni["BarrolRollRight"].length));
                //Vector3.Lerp(transform.position, transform.TransformDirection(Vector3.right * barralRollLength), 1f);
                //transform.position += transform.TransformDirection(Vector3.right * barralRollLength);
                myAni.Play("BarrolRollRight");
                // do the barrol roll to the right
                Debug.Log("BarrolRollRight has been executed.!");
            }
            if (barrolRollLeft.Check())
            {
                StartCoroutine(CombinationDelay());
                //transform.position += transform.TransformDirection(Vector3.left * barralRollLength);
                //StartCoroutine(BarralRollAnimation(false));
                //Vector3.Lerp(transform.position, transform.position + transform.TransformDirection(Vector3.left * barralRollLength), 1f);
                myAni.Play("BarrolRollLeft");
                // do the barrol roll to the left
                Debug.Log("BarrolRollLeft has been executed.!");
            }
        }
    }

    IEnumerator CombinationDelay()
    {
        canUseCombo = false;
        yield return new WaitForSeconds(1f);
        canUseCombo = true;
    }

    //IEnumerator MoveTo(Transform objectToMove, Vector3 targetPosition, float timeToTake)
    //{
    //    canMove = false;
    //    float t = 0;
    //    Vector3 originalPosition = objectToMove.position;
    //    while (t < 1)
    //    {
    //        // putting the increment of t before the lerp, will make it so that by the end of the loop our object would reach 
    //        // its target position with 100% because t is clampped between 0 to 1 in the lerp.
    //        // but if we but the increment after it - in the last run - t would become something like 0.99942 or something
    //        // we'd lerp to that - and then t would go beyond 1 and the loop would exit - and we'd end up with our object sitting at 0.99942
    //        // of the road - not 1
    //        t += Time.deltaTime / timeToTake;
    //        objectToMove.position = Vector3.Lerp(originalPosition, targetPosition, t);
    //        yield return null;
    //    }
    //    canMove = true;
    //}
}    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D myRB;
    ShipStats myShip;

    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed, maximumSpeed, currentSpeed;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myShip = GetComponent<ShipStats>();

        accSpeed = myShip.ShipAcceleration;
        maxRotationSpeed = myShip.ShipTurnSpeed;
        maxSpeed = myShip.ShipSpeedMax;

        maximumSpeed = maxSpeed * transform.up;
        currentSpeed = accSpeed * transform.up;
        //print("MaximumSpeed: " + maximumSpeed.magnitude);
        //print("CurrentSpeed: " + currentSpeed.magnitude);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            forwardSpeed = accSpeed * transform.up;
            print("CurrentSpeed: " + forwardSpeed.magnitude);
            if (forwardSpeed.y < maximumSpeed.y)
            {
                myRB.AddForce(forwardSpeed, ForceMode2D.Force);
                //print("adding Speed..");
            }
            else
            {
                forwardSpeed = maximumSpeed;
                myRB.AddForce(forwardSpeed, ForceMode2D.Force);
                print("Maximum Speed added..");
            }

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1) * maxRotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1) * -maxRotationSpeed * Time.deltaTime, Space.Self);
        }
        curSpeed = myRB.velocity.magnitude;
    }
}
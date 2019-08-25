using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D myRB;

    public float accSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;
    public float curSpeed;
    Vector3 forwardSpeed, maximumSpeed, currentSpeed;

    private void Start()
    {
        accSpeed = 30;
        maxRotationSpeed = 200;
        maxSpeed = 1000;
        myRB = GetComponent<Rigidbody2D>();
        maximumSpeed = maxSpeed * transform.up;
        currentSpeed = accSpeed * transform.up;
        print("MaximumSpeed: " + maximumSpeed.y);
        print("CurrentSpeed: " + currentSpeed.y);
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            forwardSpeed = accSpeed * transform.up;
            if (forwardSpeed.y < maximumSpeed.y)
            {
                myRB.AddForce(forwardSpeed);
                print("adding Speed..");
            }
            else
            {
                forwardSpeed = maximumSpeed;
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
        //if(curSpeed > 0)
        //    print(myRB.velocity.magnitude);
    }
}
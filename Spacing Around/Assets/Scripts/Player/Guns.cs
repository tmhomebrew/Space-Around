using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject LaserShot;
    public GameObject spawnPoint;
    public Transform LaserHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShotLaser()
    {
       //Instantiate(LaserShot, spawnPoint.transform.position, spawnPoint.transform.rotation, LaserHolder);
    }
}

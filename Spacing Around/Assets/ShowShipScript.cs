using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShipScript : MonoBehaviour
{
    ShipSelector mySS;

    [SerializeField]
    GameObject _shipToShow;

    public GameObject ShipToShow
    {
        get => _shipToShow; set
        {
            _shipToShow = value;
            if (value == null)
            {
                print(transform.name + " is not showing any ship..");
            }
            else
            {
                print(transform.name + " is showing - " + _shipToShow);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySS = GetComponentInParent<ShipSelector>();
    }
}

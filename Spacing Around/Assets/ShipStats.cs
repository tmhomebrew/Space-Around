using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public enum ShipType
    {
        Standard,
    }

    #region Field
    int shipMainBodyHealthMax;
    int shipMainBodyHealthCur;

    int shipShieldMax;
    int shipShieldCur;
    int shipRegenValue;
    bool shipRegen;

    //Movement
    float shipSpeedMax;
    float shipSpeedCur;
    float shipAcceleration;
    float shipTurnSpeed;

    int shipWeight;
    int shipCargoSpace;
    //list<Loot> - Length is shipCargoSpace... List of stored cargo

    #endregion
    
    void StatSetup()
    {
        switch (ShipType.Standard) //Should be a playerChoice..
        {
            case ShipType.Standard:

                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

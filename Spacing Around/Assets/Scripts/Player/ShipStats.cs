using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipStats : MonoBehaviour
{
    public enum ShipType
    {
        Standard,
    }

    #region Field
    //Ship Stats
    [SerializeField]
    public UIHandler UIHandlerRef;
    int shipMainBodyHealthMax;
    [SerializeField]
    int shipMainBodyHealthCur;
    bool isAlive;

    int shipShieldMax;
    [SerializeField]
    int shipShieldCur;
    int shipRegenValue;
    bool shipRegen;

    //Movement
    float shipSpeedMax;
    float shipSpeedCur;
    float shipAcceleration;
    float shipTurnSpeed;

    float shipWeight;
    float shipWeightCapacityMax;
    float shipWeightCapacityCur;
    [SerializeField]
    int shipCargoSpace;
    Inventory shipInventory;

    //Locks
    [SerializeField]
    bool isStatsSet;

    public int ShipCargoSpace
    {
        get { return shipCargoSpace; }
        set
        {
            shipCargoSpace = value;
        }
    }

    public int ShipMainBodyHealthMax
    {
        get { return shipMainBodyHealthMax; }
        set
        {
            shipMainBodyHealthMax = value;
        }
    }
    public int ShipMainBodyHealthCur
    {
        get { return shipMainBodyHealthCur; }
        set
        {
            if (!isStatsSet)
            {
                shipMainBodyHealthCur = value;
            }
            else
            {
                shipMainBodyHealthCur = value;
                UIHandlerRef.HealthVarPlayerRef = shipMainBodyHealthCur;
                if (shipMainBodyHealthCur <= 0)
                {
                    shipMainBodyHealthCur = 0;
                    print("Player_" + this.gameObject.name + " is dead..");
                }
            }
        }
    }

    public int ShipShieldMax
    {
        get { return shipShieldMax; }

        set
        {
            shipShieldMax = value;
        }
    }
    public int ShipShieldCur
    {
        get { return shipShieldCur; }

        set
        {
            shipShieldCur = value;
            UIHandlerRef.ShieldVarPlayerRef = shipShieldCur;
        }
    }

    //list<Loot> - Length is shipCargoSpace... List of stored cargo

    #endregion

    void StatSetup()
    {
        isStatsSet = false;
        switch (ShipType.Standard) //Should be a playerChoice..
        {
            case ShipType.Standard:
                ShipMainBodyHealthMax = 100;
                ShipMainBodyHealthCur = ShipMainBodyHealthMax;

                ShipShieldMax = 50;
                ShipShieldCur = 1;
                shipRegenValue = 1;
                shipRegen = false;

                //Movement
                shipSpeedMax = 10;
                //shipSpeedCur = transform.GetComponent<Rigidbody>().velocity.magnitude;
                shipAcceleration = 2;
                shipTurnSpeed = 100;

                shipWeight = 450;
                shipWeightCapacityMax = 200;
                shipWeightCapacityCur = 0;
                ShipCargoSpace = 4;
                break;
        }
        isStatsSet = true;
        isAlive = true;
    }

    private void Awake()
    {
        StatSetup();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            HealDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HealDamage(7);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(7);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">if 'regulator' is positive adds health.. else, draws health </param>
    void TakeDamage(int damage)
    {
        ShieldCalculator((int)(Mathf.Abs(damage)), true);
    }

    void HealDamage(int repair)
    {
        ShieldCalculator((int)(Mathf.Abs(repair)), false);
    }

    int remainingDamage;
    void ShieldCalculator(int incDamage, bool isDamage)
    {
        if (isDamage)
        {
            #region Taken Damage - Version 1
            for (int remainingDamage = incDamage; remainingDamage > 0; remainingDamage--)
            {
                if (ShipShieldCur > 0)
                {
                    ShipShieldCur--;
                }
                else
                {
                    if (ShipMainBodyHealthCur > 0)
                    {
                        ShipMainBodyHealthCur--;
                    }
                    else
                    {
                        print("Overkilled by: " + remainingDamage);
                        break;
                    }
                }
            }
            #endregion
        }
        else
        {
            #region Healing - Version 1
            for (int healing = incDamage; healing > 0; healing--)
            {
                if (ShipMainBodyHealthCur < ShipMainBodyHealthMax)
                {
                    ShipMainBodyHealthCur++;
                }
                else
                {
                    if (ShipShieldCur < ShipShieldMax)
                    {
                        ShipShieldCur++;
                    }
                    else
                    {
                        print("Overhealed by: " + healing);
                        break;
                    }
                }
            }
            #endregion
        }
    }
}

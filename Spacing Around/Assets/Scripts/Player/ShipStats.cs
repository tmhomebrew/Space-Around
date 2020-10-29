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

    //Shield and HP
    int shipShieldMax;
    [SerializeField]
    int shipShieldCur;
    ShieldRotater myShieldRotater;

    int shipRegenValue;
    bool shipRegen;

    //Movement
    float shipSpeedMax;
    [SerializeField]
    float shipSpeedCur;
    float shipAcceleration;
    float shipTurnSpeed;
    [SerializeField]
    bool isMoving;

    //Ship
    float shipWeight;
    float shipWeightCapacityMax;
    float shipWeightCapacityCur;
    [SerializeField]
    int shipCargoSpace;

    //Inventory
    Inventory shipInventory;
    List<BaseItem> myCargoList;

    //Locks
    [SerializeField]
    bool isStatsSet;
    GameObject attacker; //Used in shipMainBodyHealthCur..
    bool killedByPlayer;
    //list<Loot> - Length is shipCargoSpace... List of stored cargo

    #endregion
    #region Properties
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
                    IsAlive = false;
                    if (killedByPlayer)
                    {
                        attacker.GetComponent<ShipStats>().ShipInventory.GoldSize += 100;
                        attacker.GetComponent<ShipStats>().ShipInventory.Kills += 1;
                        ShipInventory.Deaths += 1;
                        print("Player_" + this.gameObject.name + " is dead.. Killed by: " + attacker.name);
                    }
                }
                else
                {
                    if (!IsAlive)
                    {
                        IsAlive = true;
                    }
                }
                killedByPlayer = false;
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
    public float ShipSpeedCur { get => shipSpeedCur; set => shipSpeedCur = value; }
    public float ShipSpeedMax { get => shipSpeedMax; set => shipSpeedMax = value; }
    public float ShipAcceleration { get => shipAcceleration; set => shipAcceleration = value; }
    public float ShipTurnSpeed { get => shipTurnSpeed; set => shipTurnSpeed = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Inventory ShipInventory { get => shipInventory; set => shipInventory = value; }
    #endregion

    void StatSetup()
    {
        isStatsSet = false;
        ShipInventory = GetComponentInChildren<Inventory>();
        myShieldRotater = GetComponentInChildren<ShieldRotater>();

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
                ShipSpeedMax = 10;
                //shipSpeedCur = transform.GetComponent<Rigidbody>().velocity.magnitude;
                ShipAcceleration = 2000;
                ShipTurnSpeed = 200;

                shipWeight = 450;
                shipWeightCapacityMax = 200;
                shipWeightCapacityCur = 0;
                ShipCargoSpace = 4;
                break;
        }
        isStatsSet = true;
        IsAlive = true;
    }

    private void Awake()
    {
        StatSetup();
    }

    // Update is called once per frame
    void Update()
    {
        CheatsAndChecks();
    }

    void CheatsAndChecks()
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
    /// <param name="i">if 'regulator' is positive adds health.. else, subtracts health </param>
    public void TakeDamage(float damage)
    {
        ShieldCalculator((int)damage, true);
    }

    public void HealDamage(int repair)
    {
        ShieldCalculator((int)repair, false);
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
                    //myShieldRotater.ShieldOnHit();
                    ShipShieldCur--;
                    continue;
                }
                else
                {
                    if (ShipMainBodyHealthCur > 0)
                    {
                        ShipMainBodyHealthCur--;
                        continue;
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

    private void OnCollisionEnter(Collision col)
    {
        print("Im hit by..: " + col.transform.name);

        if (col.gameObject.GetComponent<LaserShot>().LaserOwner != gameObject)
        {
            attacker = col.gameObject.GetComponent<LaserShot>().LaserOwner;
            killedByPlayer = true;
            //TakeDamage(col.gameObject.GetComponent<LaserShot>().    ) //<-- Need more stuff here..
            print("Should take Damage from " + attacker);
        }
    }
}

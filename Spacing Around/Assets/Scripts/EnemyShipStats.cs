using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStats : MonoBehaviour
{
    #region Fields
    //Health
    [SerializeField]
    float enemyHealthMax, enemyHealthCur;
    [SerializeField]
    float enemyShieldMax, enemyShieldCur;
    bool isAlive;

    public EnemyUIHandler eUIHandlerRef;
    public GameObject eUIHolder;

    public List<Guns> eGuns;

    //Locks
    [SerializeField]
    GameObject attacker;

    #endregion

    #region Properties
    public float EnemyHealthCur
    {
        get { return enemyHealthCur; }
        set
        {
            enemyHealthCur = value;
            eUIHandlerRef.HealthVarEnemyRef = enemyHealthCur;
            if (enemyHealthCur <= 0)
            {
                enemyHealthCur = 0;
                eUIHandlerRef.HealthVarEnemyRef = enemyHealthCur;
                isAlive = false;
                if (attacker.tag == "Player")
                {
                    print("Enemy_" + this.gameObject.name + " is dead.. Killed by: " + attacker.name);
                }
                StartCoroutine(DestroySequence());
            }
        }
    }
    public float EnemyShieldCur
    {
        get => enemyShieldCur; set
        {
            enemyShieldCur = value;
            eUIHandlerRef.ShieldVarEnemyRef = enemyShieldCur;
        }
    }
    public float EnemyHealthMax { get => enemyHealthMax; set => enemyHealthMax = value; }
    public float EnemyShieldMax { get => enemyShieldMax; set => enemyShieldMax = value; }

    #endregion

    void Awake()
    {
        if (eUIHandlerRef == null)
        {
            GetComponent<EnemyUIHandler>();
        }
        eGuns = new List<Guns>();
        eGuns.AddRange(GetComponentsInChildren<Guns>());
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;

        EnemyHealthMax = 10;
        EnemyHealthCur = EnemyHealthMax;
        EnemyShieldMax = 10;
        EnemyShieldCur = EnemyShieldMax;

        StartCoroutine(AttackAction());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Fire")
        {
            print("Im hit");
            TakeDamage(1);
            attacker = col.gameObject.GetComponent<LaserShot>().LaserOwner;
            //TakeDamage(col.gameObject.GetComponent<LaserShot>().    ) //<-- Need more stuff here..
            print("Should take Damage from " + attacker);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">if 'regulator' is positive adds health.. else, draws health </param>
    public void TakeDamage(float damage)
    {
        ShieldCalculator((int)damage, true);
    }

    int remainingDamage;
    void ShieldCalculator(int incDamage, bool isDamage)
    {
        if (isDamage)
        {
            #region Taken Damage - Version 1
            for (int remainingDamage = incDamage; remainingDamage > 0; remainingDamage--)
            {
                if (EnemyShieldCur > 0)
                {
                    EnemyShieldCur--;
                    continue;
                }
                else
                {
                    if (EnemyHealthCur > 0)
                    {
                        EnemyHealthCur--;
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
    }

    IEnumerator DestroySequence()
    {
        GetComponent<Collider2D>().enabled = false;
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
        eUIHolder.SetActive(false);
        //Animation for explosion
        LootDrop();

        yield return new WaitForSeconds(0.5f);
        Destroy(transform.root.gameObject); //Prefab
    }

    void LootDrop()
    {
        attacker.GetComponent<ShipStats>().ShipInventory.GoldSize += 50;
        attacker.GetComponent<ShipStats>().ShipInventory.Kills += 1;
    }

    IEnumerator AttackAction()
    {
        while (isAlive)
        {
            foreach (Guns g in eGuns)
            {
                g.ShotLaser();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}

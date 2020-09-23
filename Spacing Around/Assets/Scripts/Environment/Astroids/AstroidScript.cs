using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    #region Fields
    public Sprite curSprite;
    [SerializeField]
    private GameObject astroidSpawnerRef;
    private Transform myLaunchDir;
    private Rigidbody2D myRB;
    private PolygonCollider2D myCol;
    private ParticleSystem myExplosion;

    //Stats
    [SerializeField]
    private int astroidHealth;
    [SerializeField]
    bool isAlive;
    public float astroidSpeed;
    public float astroidSize;
    [SerializeField]
    private float scale;

    //Set by RenderMe.cs
    private bool astroidIsWithinRange;

    #endregion
    #region Properties

    public Transform MyLaunchDir { get => myLaunchDir; set => myLaunchDir = value; }
    public int AstroidHealth
    {
        get => astroidHealth;
        set
        {
            astroidHealth = value;
            if (astroidHealth <= 0 && isAlive)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<PolygonCollider2D>().enabled = false;
                astroidSpawnerRef.GetComponent<SpawnerAstroids>().NumberOfAstroidsInGame--; //<--Referer til SpawnPos.GO skal være AstroidSpawner.GO
                StopAllCoroutines();
                isAlive = false;
                //print("Astroid destroyed: " + transform.name);
                StartCoroutine(DestroySequence());
            }
        }
    }

    public bool AstroidIsWithinRange { get => astroidIsWithinRange; set => astroidIsWithinRange = value; }

    #endregion

    void Awake()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        myCol = GetComponent<PolygonCollider2D>();
        myRB = GetComponent<Rigidbody2D>();
        astroidSpawnerRef = GetComponentInParent<SpawnerAstroids>().gameObject;
        myExplosion = GetComponent<ParticleSystem>();
        curSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void OnEnable()
    {
        try
        {
            if (myLaunchDir == null)
            {
                MyLaunchDir = transform.root.GetComponentInChildren<PointRotater>().transform;
            }
        }
        catch (System.Exception)
        {
            print("Could not find the transform");
            throw;
        }

        scale = Mathf.Sqrt(Mathf.Pow(transform.localScale.x, 2) + Mathf.Pow(transform.localScale.y, 2));

        SetStats();
        InitialLaunch();
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (myLaunchDir == null)
            {
                MyLaunchDir = transform.root.GetComponentInChildren<PointRotater>().transform;
            }
        }
        catch (System.Exception)
        {
            print("Could not find the transform");
            throw;
        }
        
        scale = Mathf.Sqrt(Mathf.Pow(transform.localScale.x, 2) + Mathf.Pow(transform.localScale.y, 2));

        SetStats();
        InitialLaunch();
    }

    void SetStats()
    {
        astroidSize = curSprite.rect.size.magnitude / scale / 10/*ShipScale*/;
        myRB.mass = astroidSize;
        AstroidHealth = (int)(astroidSize * 2);
        astroidSpeed = 100;
        isAlive = true;
    }

    Vector2 push;

    void InitialLaunch()
    {
        push = MyLaunchDir.GetComponent<PointRotater>().AstroidDir * astroidSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    string collisionTag;
    private void OnCollisionEnter2D(Collision2D col)
    {
        collisionTag = col.transform.tag;
        //if (!AstroidIsWithinRange)
        //{
        //    return;
        //}
        if (collisionTag == "Player")
        {
            //col.gameObject.GetComponent<ShipStats>().TakeDamage(astroidSize * 10); //Damage to Player
            AstroidHealth = 0; //<-- Kills astroid
        }
        if (collisionTag == "Fire")
        {
            if (col.gameObject.GetComponent<LaserShot>().LaserOwner.tag == "Player")
            {
                col.gameObject.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize += 10;
                //print("Money: " + col.gameObject.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize);
            }
            AstroidHealth -= col.gameObject.GetComponent<LaserShot>().Damage;
            AstroidHealth = 0; //<-- Kills astroid
        }
    }

    IEnumerator DestroySequence()
    {
        myExplosion.Play();
        //Animation for astroid explosion
        //LootDrop???
        gameObject.SetActive(false); //Object Pooling
        yield return new WaitForSeconds(1.5f);
        //Destroy(gameObject);
    }
}

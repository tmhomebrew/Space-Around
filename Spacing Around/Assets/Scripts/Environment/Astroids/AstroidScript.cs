using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Sprite curSprite;
    WrapScreenHandler myWrap;
    [SerializeField]
    private Transform mySpawner;
    private Rigidbody2D myRB;
    private PolygonCollider2D myCol;
    private ParticleSystem myExplosion;

    //Stats
    private int astroidHealth;
    [SerializeField]
    bool isAlive;
    public float astroidSpeed;
    public float astroidSize;
    [SerializeField]
    private float scale;

    public Transform MySpawner { get => mySpawner; set => mySpawner = value; }
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
                StopAllCoroutines();
                isAlive = false;
                //print("Astroid destroyed: " + transform.name);
                StartCoroutine(DestroySequence());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curSprite = GetComponent<SpriteRenderer>().sprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        myWrap = GetComponent<WrapScreenHandler>();
        myRB = GetComponent<Rigidbody2D>();
        myExplosion = GetComponent<ParticleSystem>();

        scale = Mathf.Sqrt(Mathf.Pow(transform.localScale.x, 2) + Mathf.Pow(transform.localScale.y, 2));

        SetStats();
        InitialLaunch();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isAlive)
        //{
        //    StartCoroutine(myWrap.CheckVisable()); //Checks position on level..
        //}
    }

    void SetStats()
    {
        astroidSize = curSprite.rect.size.magnitude / scale / 10/*ShipScale*/;
        myRB.mass = astroidSize;
        AstroidHealth = 1;
        astroidSpeed = 100;
        isAlive = true;
    }

    void InitialLaunch()
    {
        Vector2 push = MySpawner.GetComponent<PointRotater>().AstroidDir * astroidSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<ShipStats>().TakeDamage(astroidSize * 10); //Damage to Player
            AstroidHealth = 0; //<-- Kills astroid
        }
        if (col.transform.tag == "Fire")
        {
            col.transform.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize += 10;
            //print("Money: " + col.transform.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize);
            AstroidHealth = 0; //<-- Kills astroid
        }
        if (col.transform.tag == "BoundingBox")
        {
            print("Does trigger");
            StartCoroutine(myWrap.CheckVisable());
        }
    }

    IEnumerator DestroySequence()
    {
        myExplosion.Play();
        //Animation for astroid explosion
        //LootDrop???

        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}

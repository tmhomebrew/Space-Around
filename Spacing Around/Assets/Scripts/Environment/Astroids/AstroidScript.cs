using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Sprite curSprite;
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
        myRB = GetComponent<Rigidbody2D>();
        myExplosion = GetComponent<ParticleSystem>();
        if (MySpawner == null)
        {
            try
            {
                MySpawner = transform.root.GetComponentInChildren<PointRotater>().transform;
            }
            catch (System.Exception)
            {
                print("Could not find the transform");
                throw;
            }
        }

        scale = Mathf.Sqrt(Mathf.Pow(transform.localScale.x, 2) + Mathf.Pow(transform.localScale.y, 2));

        SetStats();
        InitialLaunch();
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
            if (col.transform.GetComponent<LaserShot>().LaserOwner.name == "Player")
            {
                col.transform.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize += 10;
            }
            //print("Money: " + col.transform.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize);
            AstroidHealth = 0; //<-- Kills astroid
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

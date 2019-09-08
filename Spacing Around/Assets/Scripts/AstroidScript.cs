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
            if (astroidHealth < 0)
            {
                StopAllCoroutines();
                isAlive = false;
                astroidHealth = 0;
                print("Astroid destroyed: " + transform.name);
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


        scale = Mathf.Sqrt(Mathf.Pow(transform.localScale.x, 2) + Mathf.Pow(transform.localScale.y, 2));

        SetStats();
        InitialLaunch();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            StartCoroutine(myWrap.CheckVisable()); //Checks position on level..
        }
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
        if (col.collider.tag == "Player")
        {
            isAlive = false;
        }
    }
}

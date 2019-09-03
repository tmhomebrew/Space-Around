using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Sprite curSprite;
    WrapScreenHandler myWrap;
    [SerializeField]
    private Transform mySpawner;

    //Stats
    private int astroidHealth;
    [SerializeField]
    bool isAlive;
    public float astroidSpeed;
    public float astroidSize;
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
        scale = transform.localScale.x;

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

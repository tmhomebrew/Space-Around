using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Sprite curSprite;
    [SerializeField]
    private Transform mySpawner;

    //Stats
    public int astroidHealth;
    public float astroidSpeed;
    public float astroidSize;
    private float scale;

    public Transform MySpawner { get => mySpawner; set => mySpawner = value; }

    // Start is called before the first frame update
    void Start()
    {
        curSprite = GetComponent<SpriteRenderer>().sprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        scale = transform.localScale.x;

        SetStats();
        InitialLaunch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetStats()
    {
        astroidSize = curSprite.rect.size.magnitude / scale / 10/*ShipScale*/;
        astroidHealth = 1;
        astroidSpeed = 100;
    }

    void InitialLaunch()
    {
        Vector2 push = MySpawner.GetComponent<PointRotater>().AstroidDir * astroidSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
    }
}

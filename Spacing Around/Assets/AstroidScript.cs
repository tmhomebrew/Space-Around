using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public Sprite curSprite;

    //Stats
    public int astroidHealth;
    public float astroidSize;

    // Start is called before the first frame update
    void Start()
    {
        curSprite = GetComponent<SpriteRenderer>().sprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public enum LaserType
    {
        Green,
        Blue,
        Yellow,
        Red,
        Purple
    }

    GameObject owner;
    SpriteRenderer mySprRend;

    [SerializeField]
    public Sprite[] laserBeamSprite = new Sprite[5];
    Sprite mySprite;

    int damage = 1;
    float speed;
    public LaserType kindOfLaser;

    public GameObject Owner { get => owner; set => owner = value; }

    // Start is called before the first frame update
    void Start()
    {
        mySprRend = GetComponent<SpriteRenderer>();
        //Destroy(GetComponent<PolygonCollider2D>()); //Refreshing the Collider
        //gameObject.AddComponent<PolygonCollider2D>(); //Refreshing the Collider

        SetStats(LaserType.Green);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetStats(LaserType LT)
    {
        speed = 10;
        switch (LT)
        {
            case LaserType.Green:
                damage = 2;
                mySprite = laserBeamSprite[0];
                break;
            case LaserType.Blue:
                damage = 4;
                mySprite = laserBeamSprite[1];
                break;
            case LaserType.Yellow:
                mySprite = laserBeamSprite[2];
                break;
            case LaserType.Red:
                mySprite = laserBeamSprite[3];
                break;
            case LaserType.Purple:
                damage = 100;
                mySprite = laserBeamSprite[4];
                break;
            default:
                break;
        }
        mySprRend.sprite = mySprite;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == owner)
        {

        }
        if (col.gameObject.tag == "Player" && col.gameObject != owner)
        {
            //Apply Damage
            //Disable Collider
            //Disable Renderer
            //Display Hit-Explosion
            //Destroy LaserBeam
        }
        if (col.gameObject.tag != "Player")
        {
            Destroy(this);
        }
    }
}

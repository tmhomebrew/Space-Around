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
    [SerializeField]
    private GameObject myExplosionObj;

    //Stats
    [SerializeField]
    private int astroidHealth;
    [SerializeField]
    bool isAlive;
    public float astroidSpeed;
    public float astroidSize;
    [SerializeField]
    private float scale;

    //For InitialLaunch()
    Vector2 push;
    string collisionTag;

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
                astroidSpawnerRef.GetComponent<SpawnerAstroids>().NumberOfAstroidsInGame--; //<--Referer til SpawnPos.GO skal være AstroidSpawner.GO
                StopAllCoroutines();
                isAlive = false;
                StartCoroutine(DestroySequence());
            }
        }
    }
    #endregion

    void OnEnable()
    {
        Setup();
        InitialLaunch();
    }

    void Setup()
    {
        MyLaunchDir = transform.root.GetComponentInChildren<PointRotater>().transform;
        myRB = GetComponent<Rigidbody2D>();
        astroidSpawnerRef = GetComponentInParent<SpawnerAstroids>().gameObject;
        curSprite = GetComponent<SpriteRenderer>().sprite;
        scale = Mathf.Sqrt(curSprite.rect.width + curSprite.rect.height) / 10f;

        astroidSize = curSprite.rect.size.magnitude / (scale * 10) /*/ ShipScale*/;
        myRB.mass = astroidSize * scale;
        AstroidHealth = (int)(astroidSize * 2);
        astroidSpeed = 200;
        isAlive = true;
    }

    void InitialLaunch()
    {
        push = MyLaunchDir.GetComponent<PointRotater>().AstroidDir * astroidSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        collisionTag = col.transform.tag;

        if (collisionTag == "Player")
        {
            //col.gameObject.GetComponent<ShipStats>().TakeDamage(astroidSize * 10); //Damage to Player
            AstroidHealth = 0; //<-- Kills astroid
        }
        if (collisionTag == "Fire")
        {
            if (col.gameObject.GetComponent<LaserShot>().LaserOwner.CompareTag("Player"))
            {
                col.gameObject.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize += 10;
            }
            AstroidHealth -= col.gameObject.GetComponent<LaserShot>().Damage;
            AstroidHealth = 0; //<-- Kills astroid
        }
    }

    IEnumerator DestroySequence()
    {
        GameObject temp = Instantiate(myExplosionObj, transform.position, transform.rotation, astroidSpawnerRef.transform.GetChild(0));
        temp.GetComponent<AstroidExplosion>().OnExplosion(scale);
        //LootDrop???
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false); //Object Pooling
        yield return null;
    }
}

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
    public float astroidSpeed;
    public float astroidSize;
    [SerializeField]
    private float scale;

    //For InitialLaunch()
    private Vector2 push;
    private string collisionTag;

    #endregion
    #region Properties

    public Transform MyLaunchDir { get => myLaunchDir; set => myLaunchDir = value; }
    public int AstroidHealth
    {
        get => astroidHealth;
        set
        {
            astroidHealth = value;
            if (astroidHealth <= 0)
            {
                astroidSpawnerRef.GetComponent<SpawnerAstroids>().NumberOfAstroidsInGame--; //<--Referer til SpawnPos.GO skal være AstroidSpawner.GO
                StopAllCoroutines();
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
        if (myRB == null || myRB != GetComponent<Rigidbody2D>())
        {
            myRB = GetComponent<Rigidbody2D>();
        }
        MyLaunchDir = transform.root.GetComponentInChildren<PointRotater>().transform; //
        astroidSpawnerRef = GetComponentInParent<SpawnerAstroids>().gameObject;
        curSprite = GetComponent<SpriteRenderer>().sprite;
        scale = Mathf.Sqrt(curSprite.rect.width + curSprite.rect.height) / 10f;

        astroidSize = curSprite.rect.size.magnitude / (scale * 10); //Size of Astroid, Used for AstroidHP and Mass(Speed, RigBody)
        myRB.mass = astroidSize * scale; //Adds new mass for Astroid
        AstroidHealth = (int)(astroidSize * 2); //Astroids HP
        astroidSpeed = 200; //Astroids Speed
    }

    /// <summary>
    /// Sets and adds a force, depending on the 'AstroidDir' and 'AstroidSpeed'.
    /// The force applied is an *Impulse*.
    /// </summary>
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
            col.gameObject.GetComponent<ShipStats>().TakeDamage(astroidSize * 10); //Damage to Player
            AstroidHealth = 0; //<-- Kills astroid
        }
        if (collisionTag == "Fire")
        {
            AstroidHealth -= col.gameObject.GetComponent<LaserShot>().Damage; //Damage to Astroid
            //AstroidHealth = 0; //<-- Kills astroid
            if (AstroidHealth <= 0)
            {
                if (col.gameObject.GetComponent<LaserShot>().LaserOwner.CompareTag("Player"))
                {
                    col.gameObject.GetComponent<LaserShot>().LaserOwner.GetComponent<Inventory>().GoldSize += 10;
                }
            }
        }
    }

    /// <summary>
    /// When an Astroid is reduced to 0 HP, it will spawn a seperat new GameObject (An Explosion) with its parameters
    /// applied to the Explosion-GameObj.
    /// </summary>
    /// <returns>Makes the Astroid Inactive, returning it to the AstroidPool.</returns>
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

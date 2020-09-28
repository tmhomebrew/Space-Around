using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool isGameRunning;
    [SerializeField]

    public Transform astroidHolder;
    public float spawnTimer;
    public List<Transform> spawnPoints;

    //Astroid-settings
    [SerializeField]
    List<Sprite> astroidList = new List<Sprite>();
    List<GameObject> astroidsInGame = new List<GameObject>();
    [SerializeField]
    private int numberOfAstroidsInGame;

    //Object Pool
    private PrefabPooling myPool;
    int spawnPointIndex, astroidIndex;
    GameObject newAstroid;
    #endregion
    #region Properties
    public bool IsGameRunning
    {
        get => isGameRunning; 
        set
        {
            isGameRunning = value;
            if (isGameRunning)
            {
                StartCoroutine(Spawn());
            }
            else
            {
                StopCoroutine(Spawn());
            }
        }
    }
    public int NumberOfAstroidsInGame { get => numberOfAstroidsInGame; set => numberOfAstroidsInGame = value; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.gameObject.name.Contains("SpawnPosHolder"))
            {
                spawnPoints.Add(go.GetChild(0));
                break;
            }
        }

        //Object Pool
        myPool = GetComponent<PrefabPooling>();

        NumberOfAstroidsInGame = astroidsInGame.Count;
    }

    /// <summary>
    /// SpawnRepeater, which Runs the 'SetupNewAstroid()' with a specific interval of time.
    /// </summary>
    /// <returns>An Astroid GameObj, as long as the ObjPool has available GameObjects.</returns>
    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (myPool.GetAvailableObject() != null)
            {
                SetupNewAstroid();
            }
        }
    }


    /// <summary>
    /// Setup for a new Astroid from ObjPool.
    /// Retrives an inactive GameObj from a pool, sets it parameters and actives the GameObj.
    /// Indexs for varient properties such as; Spawn Pos and Rot, Sprite, new Collider (Matches size of GameObject) and a launch Dir.
    /// Adds the new Astroid to the total list of Astroids inGame, and adds to totalCount.
    /// Sets Astroid to Active.
    /// </summary>
    void SetupNewAstroid()
    {
        spawnPointIndex = Random.Range(0, spawnPoints.Count); //Set random SpawnPoint index
        astroidIndex = Random.Range(0, astroidList.Count); //Set random GameObject index
        newAstroid = myPool.GetAvailableObject(); //Override GameObj with new (inactive) GameObj from pool.
        newAstroid.transform.position = spawnPoints[spawnPointIndex].position; //Position
        newAstroid.transform.rotation = spawnPoints[spawnPointIndex].rotation; //Rotation
        newAstroid.transform.SetParent(astroidHolder); //Parent
        newAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex]; //Set a random Sprite
        Destroy(newAstroid.GetComponent<PolygonCollider2D>()); //Remove Collider attached to GameObj
        newAstroid.AddComponent<PolygonCollider2D>(); //Add new Collider, which fits gameObject
        newAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex]; //Sets Launch direction from SpawnPoint

        astroidsInGame.Add(newAstroid); //Adds GameObj to List
        NumberOfAstroidsInGame++; //Adds totalcount of GameObj (Alive)
        newAstroid.SetActive(true); //Activates GameObj (IsAlive)
    }
}
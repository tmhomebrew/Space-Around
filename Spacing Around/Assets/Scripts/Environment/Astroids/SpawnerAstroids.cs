using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAstroids : MonoBehaviour
{
    [SerializeField]
    private bool isGameRunning;
    [SerializeField]
    ShipStats myShip; //Test

    public GameObject enemyAstroid;
    //public GameObject astroidDir;
    public Transform astroidHolder;
    public float spawnTimer;
    public List<Transform> spawnPoints;

    //Astroid-settings
    [SerializeField]
    List<Sprite> astroidList = new List<Sprite>();
    List<GameObject> astroidsInGame = new List<GameObject>();
    [SerializeField]
    private int numberOfAstroidsInGame;

    public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
    public int NumberOfAstroidsInGame { get => numberOfAstroidsInGame; set => numberOfAstroidsInGame = value; }

    //Object Pool
    private AstroidPooling myPool;

    // Start is called before the first frame update
    void Start()
    {
        if (myShip == null)
        {
            myShip = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<ShipStats>();
        }
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.gameObject.name.Contains("SpawnPosHolder"))
            {
                spawnPoints.Add(go.GetChild(0));
            }
        }

        //Object Pool
        myPool = GetComponent<AstroidPooling>();

        NumberOfAstroidsInGame = astroidsInGame.Count;
        //InvokeRepeating("Spawn", spawnTimer, spawnTimer);

        StartCoroutine(Spawn());
    }

    int spawnPointIndex, astroidIndex;
    GameObject newAstroid;
    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (myPool.GetAvailableObject() != null)
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Count);
                astroidIndex = Random.Range(0, astroidList.Count);
                newAstroid = myPool.GetAvailableObject(); //Object
                newAstroid.transform.position = spawnPoints[spawnPointIndex].position; //Position
                newAstroid.transform.rotation = spawnPoints[spawnPointIndex].rotation; //Rotation
                newAstroid.transform.SetParent(astroidHolder); //Parent
                newAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
                Destroy(newAstroid.GetComponent<PolygonCollider2D>());
                newAstroid.AddComponent<PolygonCollider2D>();
                newAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex];

                astroidsInGame.Add(newAstroid);
                NumberOfAstroidsInGame++;
                newAstroid.SetActive(true);
            }
        }
    }

    //void Spawn()
    //{
    //    //if (!IsGameRunning || NumberOfAstroidsInGame > 10)
    //    //{
    //    //    return;
    //    //}

    //    int spawnPointIndex = Random.Range(0, spawnPoints.Count);
    //    int astroidIndex = Random.Range(0, astroidList.Count);

    //    Instantiate(enemyAstroid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, astroidHolder);
    //    enemyAstroid.GetComponent<SpriteRenderer>().sprite = astroidList[astroidIndex];
    //    enemyAstroid.GetComponent<AstroidScript>().MyLaunchDir = spawnPoints[spawnPointIndex];

    //    astroidsInGame.Add(enemyAstroid);
    //    NumberOfAstroidsInGame++;
    //}
}